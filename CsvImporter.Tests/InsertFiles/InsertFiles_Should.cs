using CsvImporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using CSVI = CsvImporter;

namespace CsvImporter.Tests.InsertFiles
{
    public class InsertFiles_Should
    {
        public static IConfiguration Configuration { get; set; }

        [Fact]
        public async Task InsertRowsToSQLAsync()
        {
            // Arrange
            var insertFiles = new CSVI.InsertFiles();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string conectionString = Configuration.GetConnectionString("DEFAULT_CONNECTION");
            string destinationTable = Configuration.GetSection("KEYS").GetSection("DESTINATION_TABLE_NAME").Value;
            int batchSize = int.Parse(Configuration.GetSection("KEYS").GetSection("BATCHSIZE").Value);

            List<Stocks> lstStock = new List<Stocks>();
            for (int i = 1; i <= 10000; i++)
            {
                Stocks stock = new Stocks();
                stock.PointOfSale = "PointOfSale" + i;
                stock.Product = "17240503103734";
                stock.Date = DateTime.Today;
                stock.Stock = 2;
                lstStock.Add(stock);
            }

            //Act
            int rowsCopied = await insertFiles.InsertBulkCsvAsync(lstStock,conectionString,destinationTable,batchSize);

            // Assert
            Assert.True(rowsCopied > 0 && rowsCopied <= 10000);
        }
    }
}
