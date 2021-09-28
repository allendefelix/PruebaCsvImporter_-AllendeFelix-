using CsvImporter.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CSVI =CsvImporter;

namespace CsvImporter.Tests.ReadFiles
{
    public class ReadFiles_Should
    {
        public static IConfiguration Configuration { get; set; }

        [Fact]
        public void ReadAllRowsFromCSVFile()
        {
            // Arrange
            var readFiles = new CSVI.ReadFiles();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string urlFile = Configuration.GetSection("KEYS").GetSection("URL_CSV_FILE").Value;

            //Act
            List<Stocks> lstStock = new List<Stocks>();
            lstStock = readFiles.ReadCsv(urlFile);

            // Assert
            Assert.True(lstStock!= null && lstStock.Count > 0);
        }
    }
}
