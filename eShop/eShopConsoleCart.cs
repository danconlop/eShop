using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business;
using Data.Enums;

namespace eShop
{
    public partial class eShopConsole
    {
        private void CartAddProducts()
        {
            string continuar = "Y";

            do
            {
                try
                {
                    // Mostrar lista de productos disponibles
                    Console.WriteLine("Lista de productos disponibles");
                    var productList = _productService.GetProducts();
                    //productList.ForEach(product => Console.WriteLine($"{product.Id}. {product.Name}"));
                    foreach (var prod in productList)
                    {
                        // Buscar si el producto ya existe en la lista
                        var productAux = _cartService.GetProductList().FirstOrDefault(p => p.Id.Equals(prod.Id));
                        // Muestra solamente si hay stock mayor a cero
                        if (productAux == null)
                        {
                            if (prod.Stock > 0) Console.WriteLine($"{prod.Id}. {prod.Name}");
                        }
                        else
                        {
                            if (productAux.Stock < prod.Stock) Console.WriteLine($"{prod.Id}. {prod.Name}");
                        }
                        
                    }

                    Console.WriteLine("Selecciona el producto a agregar");
                    var productIndex = ValidateInt(Console.ReadLine());

                    var productData = productList.ElementAt(productIndex - 1);
                    var product = new Product(productData.Id, productData.Name, productData.Price, productData.Description, productData.Brand, productData.Sku);
                    Console.WriteLine("Ingrese la cantidad de producto");
                    var productQuantity = ValidateInt(Console.ReadLine());

                    // No permite que se soliciten mas productos de los que hay en stock
                    if (productData.Stock >= productQuantity)
                    {
                        /*
                         * BUSCAR SI EL PRODUCTO NO ESTA EN EL CARRITO
                         * SI NO ESTA, ENTONCES SE AGREGA EL PRODUCTO
                         */
                        if (!_cartService.GetProductList().Any(prod => prod.Id.Equals(productIndex)))
                        {
                            // Se establece el stock
                            product.AddStock(productQuantity);
                            // Se agrega el producto
                            _cartService.AddProduct(product);
                        }
                        else // SI EL PRODUCTO YA EXISTE, SOLAMENTE SE ACTUALIZA EL STOCK
                        {
                            // validar que el stock de almacen y de carrito no tengan ya el stock posible)
                            var productAux = _cartService.GetProductList().FirstOrDefault(p => p.Id.Equals(product.Id));
                            productAux.UpdateStock(productQuantity);
                        }
                    } else
                    {
                        Console.WriteLine($"No hay suficientes unidades en stock, hay {productData.Stock} del producto seleccionado");
                    }

                    // Preguntar si se desea continuar agregando productos
                    Console.WriteLine("Desea continuar agregando productos? (S/N)");
                    continuar = Console.ReadLine();

                    if (string.IsNullOrEmpty(continuar))
                        continuar = "N";
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (continuar.ToUpper() != "N");

            Console.WriteLine("Productos agregados al carrito exitosamente");
            Console.ReadKey();

        }

        private bool CartShowDetail()
        {
            var cart = _cartService.GetProductList();

            if (cart.Any())
            {
                int index=1;
                Console.Clear();
                Console.WriteLine($"Productos en el carrito");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"#\t\tNombre\t\tCantidad\t\tPrecio");
                foreach(var prod in cart)
                {
                    Console.WriteLine($"{index}\t\t{prod.Name}\t\t{prod.Stock}\t\t{prod.Price}");
                    index++;
                }
                Console.WriteLine("------------------------");
                Console.WriteLine($"Total: {cart.Sum(product => product.Price * product.Stock)}");
                return true;
            } else
            {
                Console.WriteLine("No hay productos en el carrito");
                return false;
            }
        }

        private void CartEdit()
        {
            try
            {
                // Mostrar contenido del carrito
                if (CartShowDetail())
                {
                    Product cartProduct;
                    // Mostrar opciones
                    Console.WriteLine("Indique la opción que desea realizar");
                    Console.WriteLine("1. Editar cantidad");
                    Console.WriteLine("2. Borrar producto");
                    Console.WriteLine("3. Regresar");
                    var editOption = ValidateInt(Console.ReadLine());

                    // Solicitar el ID del producto
                    Console.WriteLine("Indique el número del producto");
                    var productIndex = ValidateInt(Console.ReadLine());

                    cartProduct = _cartService.GetProductList().ElementAt(productIndex - 1);

                    switch (editOption)
                    {
                        case 1: // Editar cantidad

                            if (cartProduct != null)
                            {
                                var whProduct = _productService.GetProduct(cartProduct.Id);
                                Console.WriteLine("Ingrese la cantidad");
                                var newQuantity = ValidateInt(Console.ReadLine());

                                if (newQuantity <= whProduct.Stock)
                                {
                                    cartProduct.AddStock(newQuantity);
                                    Console.WriteLine("Cantidad actualizada exitosamente");
                                }
                                else
                                {
                                    Console.WriteLine("No hay suficiente stock en almacén");
                                }
                            }
                            break;
                        case 2: // Eliminar producto del carrito
                            Console.WriteLine("Está seguro de eliminar el producto? (Y/N)");
                            var inputEliminar = Console.ReadLine();

                            if (!string.IsNullOrEmpty(inputEliminar))
                            {
                                if (inputEliminar.ToUpper().Equals("Y"))
                                {
                                    _cartService.GetProductList().Remove(cartProduct);
                                    Console.WriteLine("Producto removido del carrito");
                                }
                            }
                            break;
                        case 3:
                        default:
                            break;
                    }

                }
                Console.WriteLine($"\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private void EmptyCart()
        {
            Console.WriteLine("Está seguro de vaciar el carrito? (Y/N)");
            var inputVaciar = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputVaciar))
            {
                if (inputVaciar.ToUpper().Equals("Y"))
                {
                    _cartService.EmptyCart();
                    Console.WriteLine("Se ha vaciado el carrito exitosamente");
                }
            }
        }
    }
}
