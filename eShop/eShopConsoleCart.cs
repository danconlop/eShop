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
            string continuar = "S";

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

                    if (productData != null)
                    {
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
                                var productCart = _cartService.GetProductList().FirstOrDefault(p => p.Id.Equals(product.Id));
                                var stockLeft = productData.Stock - productCart.Stock;

                                if (stockLeft >= productQuantity)
                                {
                                    productCart.UpdateStock(productQuantity);
                                } else
                                {
                                    if (stockLeft > 0)
                                        Console.WriteLine($"No hay suficientes unidades en stock, hay {stockLeft} unidades del producto seleccionad");
                                    else
                                        Console.WriteLine("Producto agotado");
                                }
                            }
                        }
                        else
                        {
                            if (productData.Stock > 0)
                                Console.WriteLine($"No hay suficientes unidades en stock, hay {productData.Stock} unidades del producto seleccionado");
                            else
                                Console.WriteLine("Producto agotado");
                        }
                    } else
                    {
                        Console.WriteLine("No existe el producto indicado");
                    }

                    Console.WriteLine("Desea continuar agregando productos? (S/N)");
                    continuar = Console.ReadLine();

                    if (string.IsNullOrEmpty(continuar))
                        continuar = "N";
                }
                catch(Exception e)
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
                    // Mostrar opciones
                    Console.WriteLine("Indique la opción que desea realizar");
                    Console.WriteLine("1. Editar cantidad");
                    Console.WriteLine("2. Borrar producto");
                    Console.WriteLine("3. Regresar");
                    var editOption = ValidateInt(Console.ReadLine());

                    

                    switch (editOption)
                    {
                        case 1: // Editar cantidad
                            // Solicitar el ID del producto
                            Console.WriteLine("Indique el número del producto");
                            var editProductIndex = ValidateInt(Console.ReadLine());
                            var cartProductEdit = _cartService.GetProductList().ElementAt(editProductIndex - 1);

                            if (cartProductEdit != null)
                            {
                                var whProduct = _productService.GetProduct(cartProductEdit.Id);
                                Console.WriteLine("Ingrese la cantidad");
                                var newQuantity = ValidateInt(Console.ReadLine());

                                if (newQuantity <= whProduct.Stock)
                                {
                                    cartProductEdit.AddStock(newQuantity);
                                    Console.WriteLine("Cantidad actualizada exitosamente");
                                }
                                else
                                {
                                    Console.WriteLine("No hay suficiente stock en almacén");
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("El producto no existe en el carrito");
                            }
                            Console.ReadKey();

                            break;
                        case 2: // Eliminar producto del carrito
                            // Solicitar el ID del producto
                            Console.WriteLine("Indique el número del producto");
                            var deleteProductIndex = ValidateInt(Console.ReadLine());
                            var cartProductDelete = _cartService.GetProductList().ElementAt(deleteProductIndex - 1);

                            if (cartProductDelete != null)
                            {
                                Console.WriteLine("Está seguro de eliminar el producto? (S/N)");
                                var inputEliminar = Console.ReadLine();

                                if (!string.IsNullOrEmpty(inputEliminar))
                                {
                                    if (inputEliminar.ToUpper().Equals("S"))
                                    {
                                        _cartService.GetProductList().Remove(cartProductDelete);
                                        Console.WriteLine("Producto removido del carrito");
                                    }
                                }
                            } else
                            {
                                Console.WriteLine("El producto no existe en el carrito");
                            }
                            Console.ReadKey();

                            break;
                        default:
                            break;
                    }
                }
                
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private void EmptyCart()
        {
            Console.WriteLine("Está seguro de vaciar el carrito? (S/N)");
            var inputVaciar = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputVaciar))
            {
                if (inputVaciar.ToUpper().Equals("S"))
                {
                    _cartService.EmptyCart();
                    Console.WriteLine("Se ha vaciado el carrito exitosamente");
                }
            }
        }
    }
}
