using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaAA.Models;

namespace PruebaAA.Collections
{
    public static class InventoryCollection
    {
        /// <summary>
        /// Save the data in the database.
        /// </summary>
        /// <param name="inventoryList">List of Inventory object.</param>
        public static async Task SaveAsync(IEnumerable<Inventory> inventoryList)
        {
            await using var context = new DBContext();
            await context.BulkInsertAsync(inventoryList);
        }

        /// <summary>
        /// Remove exiting data in the database.
        /// </summary>
        public static async Task RemoveAsync()
        {
            await using var context = new DBContext();
            await context.BulkDeleteAsync(context.Inventory);
        }

        /// <summary>
        /// Get a list of elements from the database.
        /// </summary>
        ///<param name="count">Number of items to return.</param>
        public static async Task<IEnumerable<Inventory>> GetAsync(int count)
        {
            await using var context = new DBContext();
            return context.Inventory.Take(count).ToList();
        }
    }
}
