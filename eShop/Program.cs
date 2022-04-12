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
            Console.WriteLine("3. Consultar productos");
            Console.WriteLine("4. Consultar producto");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Salir");

            switch (Console.ReadLine())
            {
                case "1":
                    AgregarProducto();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                default:
                    return false;
                    break;
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
    }
}
