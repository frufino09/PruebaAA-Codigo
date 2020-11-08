using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PruebaAA.Models;
using PruebaAA.Services;

namespace PruebaAA
{
    internal static class Program
    {
        private const string Url = "https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV";

        private static async Task Main()
        {
            //Remove exist data
            await RemoveExistData();

            //Reading process 
            var inventoryList = await ReadAndTransformAllData();

            //Saving process 
            await SaveData(inventoryList);

            //Get Elements from database
            var elements = await GetData(5);
            foreach (var element in elements)
            {
                Console.WriteLine("Id: {0}, PointOfSale: {1}, Product: {2}, Date: {3:yyyy-MM-dd}, Stock: {4}", 
                                  element.Id, element.PointOfSale, element.Product, element.Date, element.Stock);
            }
            Console.WriteLine("...");
            Console.WriteLine("process completed successfully.");
            Console.ReadKey();
        }

        private static async Task RemoveExistData()
        {
            Console.WriteLine("Removing existing data from database...");
            var removeExecution = new Stopwatch();
            removeExecution.Start();

            await InventoryService.RemoveAsync();

            removeExecution.Stop();
            var removeTimeSpan = removeExecution.Elapsed;
            Console.WriteLine("Removing data completed. Time: {0}h {1}m {2}s {3}ms",
                removeTimeSpan.Hours, removeTimeSpan.Minutes, removeTimeSpan.Seconds, removeTimeSpan.Milliseconds);
        }

        private static async Task<IEnumerable<Inventory>> ReadAndTransformAllData()
        {
            Console.WriteLine("Reading and transforming data from the file...");
            var readExecution = new Stopwatch();
            readExecution.Start();
            
            var inventoryList = await InventoryService.ReadAndTransformFileAsync(Url);
            
            readExecution.Stop();
            var readTimeSpan = readExecution.Elapsed;
            Console.WriteLine("Reading and transformation completed. Time: {0}h {1}m {2}s {3}ms",
                readTimeSpan.Hours, readTimeSpan.Minutes, readTimeSpan.Seconds, readTimeSpan.Milliseconds);

            return inventoryList;
        }

        private static async Task SaveData(IEnumerable<Inventory> inventoryList)
        {
            Console.WriteLine("Saving data to database...");
            var saveExecution = new Stopwatch();
            saveExecution.Start();

            await InventoryService.SaveAsync(inventoryList);

            saveExecution.Stop();
            var saveTimeSpan = saveExecution.Elapsed;
            Console.WriteLine("Saving data completed. Time: {0}h {1}m {2}s {3}ms",
                saveTimeSpan.Hours, saveTimeSpan.Minutes, saveTimeSpan.Seconds, saveTimeSpan.Milliseconds);
        }

        private static async Task<IEnumerable<Inventory>> GetData(int count)
        {
            return await InventoryService.GetAsync(count);
        }
    }
}
