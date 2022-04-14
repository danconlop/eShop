using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Services.Abstractions
{
    public interface ICartService
    {
        public void AddProduct(Product product);
        public void UpdateProductQuantity(Product product, int newQuantity);
        public void DeleteProduct(Product product);
        public List<Product> GetProductList();
        public void EmptyCart();

    }
}
