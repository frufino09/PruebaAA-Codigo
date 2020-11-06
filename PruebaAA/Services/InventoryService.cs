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

                if(line == null || string.IsNullOrWhiteSpace(line) || line.Equals("PointOfSale;Product;Date;Stock"))
                    continue;

                var lineParse = line.Split(';');
                resultInventoryList.Add(new Inventory
                {
                    PointOfSale = lineParse[0],
                    Product = lineParse[1],
                    Date = DateTime.ParseExact(lineParse[2], "yyyy-MM-dd",  CultureInfo.InvariantCulture),
                    Stock = int.Parse(lineParse[3])
                });
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
