using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Abstractions;
using Data.Entities;

namespace Business.Services.Implementations
{
    public class CartService : ICartService
    {
        private List<Product> _cartProducts = new List<Product>();

        public void AddProduct(Product product)
        {
            _cartProducts.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            var productAux = _cartProducts.FirstOrDefault(p => p.Id.Equals(product.Id));

            if (productAux == null)
                throw new InvalidOperationException("El producto seleccionado no existe en el carrito");

            _cartProducts.Remove(productAux);
        }

        public void EmptyCart() => _cartProducts.Clear();

        public List<Product> GetProductList()
        {
            return _cartProducts;
        }

        public void UpdateProductQuantity(Product product, int newQuantity)
        {
            var productAux = _cartProducts.FirstOrDefault(p => p.Id.Equals(product.Id));

            if (productAux == null)
                throw new InvalidOperationException("El producto seleccionado no existe en el carrito");

            productAux.UpdateStock(newQuantity);

        }
    }
}
