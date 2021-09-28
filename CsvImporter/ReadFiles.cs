using CsvHelper;
using CsvHelper.Configuration;
using CsvImporter.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace CsvImporter
{
    public class ReadFiles
    {
        private readonly ILogger<ReadFiles> _logger;

        public ReadFiles()
        {
        }

        public ReadFiles(ILogger<ReadFiles> logger)
        {
            this._logger = logger;
        }

        public List<Stocks> ReadCsv(string urlFile)
        {
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(urlFile);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            List<Stocks> lstStock = new List<Stocks>();
            try
            {
                using (StreamReader reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, config))
                {
                    lstStock = csv.GetRecords<Stocks>().ToList();
                    _logger?.LogInformation($" Se leyeron {lstStock.Count} registros del archivo {urlFile}.");
                    return lstStock;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return lstStock;
            }
        }
    }
}
