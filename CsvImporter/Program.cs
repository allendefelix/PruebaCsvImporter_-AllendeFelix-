using CsvImporter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvImporter
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var readFiles = serviceProvider.GetService<ReadFiles>();
            var deleteFiles = serviceProvider.GetService<DeleteFiles>();
            var insertFiles = serviceProvider.GetService<InsertFiles>();


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            string urlFile = Configuration.GetSection("KEYS").GetSection("URL_CSV_FILE").Value;
            List<Stocks> lstStock = new List<Stocks>();
            lstStock = readFiles.ReadCsv(urlFile);

            if (lstStock != null && lstStock.Any())
            {
                string conectionString = Configuration.GetConnectionString("DEFAULT_CONNECTION");
                string destinationTable = Configuration.GetSection("KEYS").GetSection("DESTINATION_TABLE_NAME").Value;
                int batchSize = int.Parse(Configuration.GetSection("KEYS").GetSection("BATCHSIZE").Value);
                deleteFiles.DeleteAllFiles(conectionString, destinationTable);
                await insertFiles.InsertBulkCsvAsync(lstStock, conectionString, destinationTable, batchSize);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
            .AddScoped<ReadFiles>()
            .AddScoped<DeleteFiles>()
            .AddScoped<InsertFiles>();
        }
    }
}
