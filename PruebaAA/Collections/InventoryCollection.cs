using System.Collections.Generic;
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
    }
}
