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

        /* Modifiqué el borrado porque no tiene logica que se solicite todo el objeto
         * cuando solamente se necesita saber el ID a eliminar
         */
        //public void DeleteProduct(Product product)
        public void DeleteProduct(int id)
        {
            /* Así lo tenia yo, funciona y también es correcto */
            //var itemIndex = ProductList.IndexOf(ProductList.Find(p => p.Id == product.Id));
            //ProductList.RemoveAt(itemIndex);

            /* De esta manera se asegura de que en verdad exista el ID ingresado
             * por lo que me parece más segura y adecuada
             */
            var entity = ProductList.FirstOrDefault(p => p.Id == id);

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
    }
}
