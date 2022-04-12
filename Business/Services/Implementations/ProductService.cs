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
        public static List<Product> ProductList = new List<Product>();
        public void AddProduct(Product product)
        {
            ProductList.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            //var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == product.Id));
            //ProductList.RemoveAt(itemIndex);
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
            //var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == product.Id));
            //ProductList[itemIndex] = product;

            var entity = ProductList.FirstOrDefault(p => p.Id == product.Id);

            if (entity != null)
                entity.Update(product.Name, product.Description, product.Price);
            else
                throw new ApplicationException("El producto no fue encontrado");
        }
    }
}
