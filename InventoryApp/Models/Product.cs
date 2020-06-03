using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryApp.Models
{
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }

        public override string ToString()
        {
            return this.Name + " (" + this.Quantity + ")";
        }
    }
}
