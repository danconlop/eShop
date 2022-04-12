using System;
using Business.Services.Implementations;
using Data.Entities;

namespace eShop
{

    class Program
    {
        private static ProductService _productService;

        static void Main(string[] args)
        {
            _productService = new ProductService();

            _productService.AddProduct(new Product(1, "Silla", 150.00m, "Silla ergonomica", "Costco", "SDD123"));
            _productService.AddProduct(new Product(2, "Mesa", 200.00m, "Mesa de comedor madera", "MesaTJ", "MS66623"));
            _productService.AddProduct(new Product(3, "Estufa", 3500m, "Estufa empotrable", "Mabe", "ES9685"));

            var showMenu = true;

            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Editar producto");
            Console.WriteLine("3. Lista de productos");
            Console.WriteLine("4. Consultar producto");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Salir");

            switch (Console.ReadLine())
            {
                case "1": // Agregar
                    AgregarProducto();
                    break;
                case "2": // Editar
                    EditarProducto();
                    break;
                case "3": // Consultar productos
                    MostrarProductos();
                    Console.ReadKey();
                    break;
                case "4": // Consultar un producto
                    ConsultarProducto();
                    break;
                case "5": // Eliminar un producto
                    EliminarProducto();
                    break;
                case "6":
                default:
                    return false;
            }

            return true;
        }

        private static void AgregarProducto()
        {
            Console.WriteLine("Agrega los valores necesarios para registrar un producto");
            Console.WriteLine("Id:");
            var id = Console.ReadLine();
            Console.WriteLine("Nombre:");
            var name = Console.ReadLine();
            Console.WriteLine("Precio:");
            var price = Console.ReadLine();
            Console.WriteLine("Descripcion:");
            var description = Console.ReadLine();
            Console.WriteLine("Marca:");
            var brand = Console.ReadLine();
            Console.WriteLine("SKU:");
            var sku = Console.ReadLine();

            try
            {
                if (!Int32.TryParse(id, out int idAux))
                {
                    throw new ApplicationException("No se pudo castear el ID correctamente");
                }

                if (!decimal.TryParse(price, out decimal priceAux))
                {
                    throw new ApplicationException("El precio es inválido");
                }

                var product = new Product(idAux, name, priceAux, description, brand, sku);
                _productService.AddProduct(product);
                Console.WriteLine("Producto agregado correctamente");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MostrarProductos()
        {
            Console.Clear();
            foreach(var product in _productService.GetProducts())
            {
                Console.WriteLine($"ID: {product.Id} Name: {product.Name}");
            }
        }

        private static void ConsultarProducto()
        {
            MostrarProductos();
            Console.WriteLine("Indique el ID del producto que desea consultar");
            var id = Console.ReadLine();

            try
            {
                if (!Int32.TryParse(id, out int idAux))
                {
                    throw new ApplicationException("No se pudo castear el ID correctamente");
                }

                Product product = _productService.GetProduct(idAux);

                Console.WriteLine($"ID: {product.Id} Name: {product.Name} Description: {product.Description} Brand: {product.Brand} Price: {product.Price} Stock: {product.Stock}");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void EditarProducto()
        {
            MostrarProductos();
            Console.WriteLine("\nIngresa los valores necesarios para editar el producto");
            Console.WriteLine("Ingrese el id del producto a editar:");
            var id = Console.ReadLine();
            Console.WriteLine("Nombre:");
            var name = Console.ReadLine();
            Console.WriteLine("Precio:");
            var price = Console.ReadLine();
            Console.WriteLine("Descripcion:");
            var description = Console.ReadLine();
            Console.WriteLine("Marca:");
            var brand = Console.ReadLine();
            Console.WriteLine("SKU:");
            var sku = Console.ReadLine();

            try
            {
                if (!Int32.TryParse(id, out int idAux))
                {
                    throw new ApplicationException("No se pudo castear el ID correctamente");
                }

                if (!decimal.TryParse(price, out decimal priceAux))
                {
                    throw new ApplicationException("El precio es inválido");
                }

                var product = new Product(idAux, name, priceAux, description, brand, sku);
                _productService.UpdateProduct(product);
                Console.WriteLine("Producto actualizado correctamente");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void EliminarProducto()
        {
            MostrarProductos();
            Console.WriteLine("Indique el ID del producto que desea eliminar");
            var id = Console.ReadLine();

            try
            {
                if (!Int32.TryParse(id, out int idAux))
                {
                    throw new ApplicationException("No se pudo castear el ID correctamente");
                }

                _productService.DeleteProduct(idAux);
                Console.WriteLine("Producto eliminado correctamente");
                Console.ReadKey();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
