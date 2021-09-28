using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CSVI = CsvImporter;

namespace CsvImporter.Tests.DeleteFiles
{
    public class DeleteFiles_Should
    {
        public static IConfiguration Configuration { get; set; }

        [Fact]
        public void DeleteAllFilesTableStocks()
        {
            // Arrange
            var deleteFiles = new CSVI.DeleteFiles();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string conectionString = Configuration.GetConnectionString("DEFAULT_CONNECTION");
            string destinationTable = Configuration.GetSection("KEYS").GetSection("DESTINATION_TABLE_NAME").Value;

            //Act
            object rowsDeleted = deleteFiles.DeleteAllFiles(conectionString, destinationTable);

            // Assert
            Assert.Null(rowsDeleted);
        }
    }
}
