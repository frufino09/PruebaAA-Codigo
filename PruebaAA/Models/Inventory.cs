using System;

namespace PruebaAA.Models
{
    public partial class Inventory
    {
        public long Id { get; set; }
        public string PointOfSale { get; set; }
        public string Product { get; set; }
        public DateTime Date { get; set; }
        public int Stock { get; set; }
    }
}
