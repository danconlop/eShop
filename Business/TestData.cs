using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Abstractions;
using Data.Entities;

namespace Business
{
    public static class TestData
    {
        public static List<Product> ProductList = new List<Product>
        {
            new Product(1, "Silla", 150, "Silla ergonomica", "Costco", "SDD123",4),
            new Product(2, "Mesa", 200, "Mesa de comedor madera", "Costco", "MS66623",5),
            new Product(3, "Estufa", 3500, "Estufa empotrable", "Mabe", "ES9685",12)
        };

        public static List<Department> DepartmentList = new List<Department>
        {
            new Department(1,"Electronicos", new List<Subdepartment>{
                new Subdepartment(1,"TVs"),
                new Subdepartment(2,"Celulares"),
                new Subdepartment(3,"Audio")
            }),
            new Department(2,"Muebles", new List<Subdepartment>{
                new Subdepartment(1,"Cocina"),
                new Subdepartment(2,"Comedor"),
                new Subdepartment(3,"Sala")
            }),
            new Department(3,"Alimentos", new List<Subdepartment>{
                new Subdepartment(1,"Lacteos"),
                new Subdepartment(2,"Carnes frias"),
                new Subdepartment(3,"Pastas")
            })
        };

        //public static List<Provider> ProviderList = new List<Provider>;

        public static List<Provider> GetProviders()
        {
            var providers = new List<Provider>();

            var p1 = new Provider(1, "Gamesa", "proveedor@gamesa.com");
            p1.AddAddress("Islas 123", "Mexicali");
            p1.AddPhoneNumber("6861234567");
            providers.Add(p1);

            var p2 = new Provider(2, "Levis", "proveedor@levis.com");
            p1.AddAddress("Islas Levis 123", "Tijuana");
            p1.AddPhoneNumber("6641234656");
            providers.Add(p2);

            var p3 = new Provider(3, "Mercado Chuchita", "proveedor@chuchita.com");
            p1.AddAddress("Islas Chu 123", "Tijuana");
            p1.AddPhoneNumber("6641231644");
            providers.Add(p3);

            return providers;
        }
    }
}
