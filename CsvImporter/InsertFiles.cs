using CsvImporter.Models;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class InsertFiles
    {
        private readonly ILogger<InsertFiles> _logger;
        public InsertFiles(ILogger<InsertFiles> logger)
        {
            this._logger = logger;
        }

        public InsertFiles()
        { }

        public  async Task<int>  InsertBulkCsvAsync(List<Stocks> lstStocks, string conectionString, string destinationTable, int batchSize)
        {
            var copyParameters = new[]
           {
                   nameof(Stocks.PointOfSale),
                   nameof(Stocks.Product),
                   nameof(Stocks.Date),
                   nameof(Stocks.Stock)
                  };

            using (var sqlCopy = new SqlBulkCopy(conectionString))
            {
                sqlCopy.DestinationTableName = destinationTable;
                sqlCopy.BatchSize = batchSize;

                try
                {
                    using (var sqlReader = ObjectReader.Create(lstStocks, copyParameters))
                    {
                        await sqlCopy.WriteToServerAsync(sqlReader);
                        _logger?.LogInformation($" Se insertaron en la tabla {sqlCopy.DestinationTableName}  {sqlCopy.RowsCopied}  registros.");
                        sqlCopy.Close();
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex.Message.ToString());
                }
                return sqlCopy.RowsCopied;
            }
        }
    }
}

