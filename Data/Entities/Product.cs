using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Stock { get; private set; }
        public decimal Price { get; private set; }
        public string Sku { get; private set; }
        public string Description { get; private set; }
        public string Brand { get; private set; }
        private Subdepartment Subdepartment { get; set; }

        public Product(int id, string name, decimal price, string description, string brand, string sku, int stock = 1)
        {
            if (price < 0)
                throw new InvalidOperationException("El precio no puede ser menor a cero");

            if (stock <= 0)
                throw new InvalidOperationException("El stock tiene que ser mayor a uno");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("El nombre no puede ser vacio");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("La descripción no puede ser vacia");

            if (string.IsNullOrEmpty(brand))
                throw new ArgumentException("La marca no puede ser vacia");

            if (string.IsNullOrEmpty(sku))
                throw new ArgumentException("El SKU no puede ser vacio");

            
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Brand = brand;
            Sku = sku;
            Stock = stock;
        }
        public void Update(string name, string description, decimal price)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
