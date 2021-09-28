using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;

namespace CsvImporter
{
    public class DeleteFiles
    {
        private readonly ILogger<DeleteFiles> _logger;
        public DeleteFiles(ILogger<DeleteFiles> logger)
        {
            this._logger = logger;
        }

        public DeleteFiles()
        { 
        }

        public object DeleteAllFiles(string conectionString, string destinationTable)
        {
            SqlConnection con = new SqlConnection(conectionString);
            string s = $"Truncate Table {destinationTable}";
            SqlCommand Com = new SqlCommand(s, con);
            object firstRowStockTable = null;
            try
            {
                con.Open();
                firstRowStockTable = Com.ExecuteScalar();
                _logger?.LogInformation($" Se eliminaron todos los registros de la tabla {destinationTable}.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message.ToString());
            }
            return firstRowStockTable;
        }
    }
}
