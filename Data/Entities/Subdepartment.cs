using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Subdepartment
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Department Department { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public Subdepartment(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("El nombre no puede ser vacio");
            Id = id;
            Name = name;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
