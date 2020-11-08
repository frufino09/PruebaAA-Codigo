using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PruebaAA.Collections;
using PruebaAA.Models;

namespace PruebaAA.Services
{
    public static class InventoryService
    {
        private static char separator = ';';

        /// <summary>
        /// Transform string to Inventory object
        /// </summary>
        /// <param name="line">line string</param>
        private static Inventory ProcessLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.Equals("PointOfSale;Product;Date;Stock"))
            {
                return null;
            }

            var lineParse = line.Split(separator);
            return new Inventory
            {
                PointOfSale = lineParse[0],
                Product = lineParse[1],
                Date = DateTime.TryParse("yyyy-MM-dd", out var date) ? date : DateTime.Now,
                Stock = int.Parse(lineParse[3])
            };
        }

        /// <summary>
        /// Read and transform the data in the inventory objects list from a csv file
        /// </summary>
        /// <param name="url">Url path of the csv file</param>
        public static async Task<IEnumerable<Inventory>> ReadAndTransformFileAsync(string url)
        {
            var resultInventoryList = new List<Inventory>();

            var request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            var response = await request.GetResponseAsync();
            using var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                var inventory = ProcessLine(line);
                if (inventory != null)
                {
                    resultInventoryList.Add(inventory);
                }
            }

            return resultInventoryList;
        }

        /// <summary>
        /// Remove exiting data in the database.
        /// </summary>
        public static async Task RemoveAsync()
        {
            await InventoryCollection.RemoveAsync();
        }

        /// <summary>
        /// Save the data in the database.
        /// </summary>
        /// <param name="inventoryList">List of Inventory object.</param>
        public static async Task SaveAsync(IEnumerable<Inventory> inventoryList)
        {
            await InventoryCollection.SaveAsync(inventoryList);
        }
    }
}
