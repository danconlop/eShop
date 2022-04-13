using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Abstractions;
using Data.Entities;


namespace Business.Services.Implementations
{
    public class ProductService : IProductService
    {
        public static List<Product> ProductList = new List<Product>
        {
            new Product(1, "Silla", 150, "Silla ergonomica", "Costco", "SDD123",4),
            new Product(2, "Mesa", 200, "Mesa de comedor madera", "Costco", "MS66623",5),
            new Product(3, "Estufa", 3500, "Estufa empotrable", "Mabe", "ES9685",12)
        };

        public List<Department> DepartmentList = new List<Department>
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

        public void AddProduct(Product product)
        {
            ProductList.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            /* Así lo tenia yo, funciona y también es correcto */
            //var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == product.Id));
            //ProductList.RemoveAt(itemIndex);

            /* De esta manera se asegura de que en verdad exista el ID ingresado
             * por lo que me parece más segura y adecuada
             */
            var entity = ProductList.FirstOrDefault(p => p.Id == product.Id);

            if (entity != null)
                ProductList.Remove(entity);
            else
                throw new ApplicationException("El producto no fue encontrado");
        }

        public Product GetProduct(int id)
        {
            var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == id));
            return ProductList.ElementAt(itemIndex);

            //var entity = ProductList.FirstOrDefault(p => p.Id == id);

            //if (entity != null)
            //    return entity;
        }

        public List<Product> GetProducts()
        {
            return ProductList;
        }

        public void UpdateProduct(Product product)
        {
            // Así lo tenía yo, pero de esta manera se actualiza todo el registro
            /*
            var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == product.Id));
            ProductList[itemIndex] = product;
            */

            /* De esta manera, se debe crear un metodo en la clase que permita actualizar solamente
             * lo que se quiere permitir actualizar
             */
            var entity = ProductList.FirstOrDefault(p => p.Id == product.Id);

            if (entity != null)
                entity.Update(product.Name, product.Description, product.Price);
            else
                throw new ApplicationException("El producto no fue encontrado");
        }

        public List<Department> GetDepartments()
        {
            return DepartmentList;
        }

        public List<Subdepartment> GetSubdepartments(int departmentID)
        {
            var result = DepartmentList.FirstOrDefault(department => department.Id.Equals(departmentID));

            if (result != null)
                return result.Subdepartments;

            return new List<Subdepartment>();
        }
    }
}
