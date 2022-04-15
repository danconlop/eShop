using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;

namespace eShop
{
    public partial class eShopConsole
    {
        public void AddOrder()
        {
            // Validar que el carrito tenga productos
            if (_cartService.GetProductList().Any())
            {
                Console.WriteLine("Se generará su pedido, está seguro de finalizar su compra (Y/N)");
                var inputRealizarPedido = Console.ReadLine();

                if (!string.IsNullOrEmpty(inputRealizarPedido))
                {
                    if (inputRealizarPedido.ToUpper().Equals("Y"))
                    {

                        // Crear nuevo pedido
                        List<Product> orderProducts = new List<Product>();
                        foreach(var product in _cartService.GetProductList())
                        {
                            orderProducts.Add(new Product(
                                product.Id,
                                product.Name,
                                product.Price,
                                product.Description,
                                product.Brand,
                                product.Sku,
                                product.Stock
                                ));
                        }
                        Order newOrder = new Order(_orderService.GetOrders().Count + 1, orderProducts, DateTime.Now);

                        // Actualizar stock en productos de almacen (_productlist)
                        foreach (var prod in newOrder.OrderProducts)
                        {
                            var prodAux = _productService.GetProducts().FirstOrDefault(product => product.Id.Equals(prod.Id));

                            _productService.SubstrackStock(prodAux, prod.Stock);
                        }
                        // Generar pedido
                        _orderService.AddOrder(newOrder);
                        // Vaciar carrito
                        _cartService.EmptyCart();
                        Console.WriteLine("Pedido generado exitosamente");
                    }
                }
            }
            else
            {
                Console.WriteLine("No puede generarse un pedido si no hay productos en el carrito");
            }

            Console.ReadKey();
        }

        public void ShowOrders()
        {
            var listOrders = _orderService.GetOrders();
            decimal grandTotal = 0;
            int index = 1;

            if (listOrders.Any())
            {
                foreach(var order in listOrders)
                {
                    grandTotal += order.Total;
                    Console.WriteLine($"Pedido # {order.Id}");
                    Console.WriteLine($"Fecha {order.OrderDate.ToShortDateString()}");
                    Console.WriteLine($"Total {order.Total}");
                    Console.WriteLine("------------------- PRODUCTS -----------------------");
                    Console.WriteLine($"#\t\tNombre\t\tCantidad\t\tPrecio unitario\t\tTotal");
                    foreach (var product in order.OrderProducts)
                    {
                        Console.WriteLine($"{index}\t\t{product.Name}\t\t{product.Stock}\t\t{product.Price}\t\t{product.Stock * product.Price}");
                        index++;
                    }
                    Console.WriteLine("----------------------------------------------------");
                }
                Console.WriteLine($"\nTotal gastado: {grandTotal}");

            } else
            {
                Console.WriteLine("No hay pedidos");
            }

            Console.ReadKey();
        }
    }
}
