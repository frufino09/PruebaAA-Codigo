using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaAA.Models
{
    public partial class Inventory
    {
        private const char Separator = ';';

        public static Inventory FromString(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.Equals("PointOfSale;Product;Date;Stock"))
            {
                return null;
            }

            var lineParse = line.Split(Separator);
            if (lineParse.Length != 4)
            {
                return null;
            }

            return new Inventory
            {
                PointOfSale = lineParse[0],
                Product = lineParse[1],
                Date = DateTime.TryParse("yyyy-MM-dd", out var date) ? date : DateTime.Now,
                Stock = int.TryParse(lineParse[3], out var stock) ? stock : 0
            };
        }
    }
}
