using System;
using System.Linq;
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
            Console.WriteLine("3. Lista de productos");
            Console.WriteLine("4. Consultar producto");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Reportes");
            Console.WriteLine("7. Salir del sistema");

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
                case "6": // Reportes
                    MenuDeReportes();
                    break;
                case "7":
                default:
                    return false;
            }

            return true;
        }

        private static void MenuDeReportes()
        {
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. Top 5 de productos más caros ordenados por precio más alto");
            Console.WriteLine("2. Productos con 5 unidades o menos ordenados por unidades");
            Console.WriteLine("3. Nombre de productos por marcas ordenados por nombre");
            Console.WriteLine("4. Agrupación de departamentos con subdepartamentos y productos");
            Console.WriteLine("5. Regresar");

            switch (Console.ReadLine())
            {
                case "1": // Top 5 de productos más caros ordenados por precio más alto
                    Reports.TopFive();
                    break;
                case "2": // Productos con 5 unidades o menos ordenados por unidades
                    Reports.FiveUnitsOrdered();
                    break;
                case "3": // Nombre de productos por marcas ordenados por nombre
                    Reports.BrandOrderedByName();
                    break;
                case "4": // Agrupación de departamentos con subdepartamentos y productos
                    Reports.GroupByDepartmentSubdepartmentAndProduct();
                    break;
                default:
                    return;
                    
            }
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
                var subdepartment = SolicitarSubdepartamento();

                product.AddSubdepartment(subdepartment);
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
                Console.WriteLine(product.ToString());
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

                Console.WriteLine(product.ToString());
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

                Product product = _productService.GetProduct(idAux);
                // EL OBTENER PRODUCTO YA TIENE UNA CLASE QUE REGRESA UN OBJETO, CORREGIR ESTA PARTE
                
                //var product = new Product(idAux, name, priceAux, description, "BRAND", "SKU");
                _productService.UpdateProduct(product);
                
                //_productService.UpdateProduct(idAux, name, description, priceAux);
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

                Product product = _productService.GetProduct(idAux);

                _productService.DeleteProduct(product);
                Console.WriteLine("Producto eliminado correctamente");
                Console.ReadKey();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MostrarDepartamentos()
        {
            foreach(var department in _productService.GetDepartments())
            {
                Console.WriteLine($"{department.Id}. {department.Name}");
            }
        }

        private static Subdepartment SolicitarSubdepartamento()
        {
            Console.WriteLine("Elije el departamento");
            var departments = _productService.GetDepartments();
            foreach (var element in departments)
            {
                Console.WriteLine($"{element.Id}. {element.Name}");
            }
            var departmentIndex = ValidateInt(Console.ReadLine()) - 1;
            var department = departments.ElementAt(departmentIndex);
            // Subdepartments
            Console.WriteLine($"Elija el subdepartamento de {departments.ElementAt(departmentIndex).Name}");
            foreach(var element in department.Subdepartments)
            {
                Console.WriteLine($"{element.Id}. {element.Name}");
            }
            var subdepartmentIndex = ValidateInt(Console.ReadLine())-1;
            var subdepartment = department.Subdepartments.ElementAt(subdepartmentIndex);
            subdepartment.Department = department;

            return subdepartment;
        }

        public static int ValidateInt(string input)
        {
            if (Int32.TryParse(input, out int outputInt))
                return outputInt;
            else
                throw new InvalidCastException("Debe ingresar un número");
        }
        
    }

    public class Reports
    {
        private static ProductService _productService = new ProductService();
        public static void TopFive()
        {
            // Top 5 de productos más caros ordenados por precio más alto
            var result = _productService.GetProducts()
                .OrderByDescending(product => product.Price)
                .Take(5);

            foreach (var product in result)
            {
                Console.WriteLine(product.ToString());
            }
            Console.ReadKey();
        }

        public static void FiveUnitsOrdered()
        {
            // Productos con 5 unidades o menos ordenados por unidades
            var result = _productService.GetProducts()
                .Where(product => product.Stock <= 5)
                .OrderByDescending(product => product.Stock);

            foreach (var product in result)
            {
                Console.WriteLine(product.ToString());
            }
            Console.ReadKey();
        }

        public static void BrandOrderedByName()
        {
            //Nombre de productos por marcas ordenados por nombre
            var result = _productService.GetProducts()
                .OrderBy(product => product.Name)
                .GroupBy(product => product.Brand);

            foreach (var brand in result)
            {
                Console.WriteLine($"Marca [{brand.Key}]");
                foreach (var product in brand)
                {
                    Console.WriteLine(product.ToString());
                }
            }

            Console.ReadKey();
        }

        public static void GroupByDepartmentSubdepartmentAndProduct()
        {
            //Agrupación de departamentos con subdepartamentos y productos
            var departments = _productService.GetDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine($"Department {department.Name} >");
                var subdepartments = _productService.GetSubdepartments(department.Id);
                foreach (var subdepartment in subdepartments)
                {
                    Console.WriteLine($"Subdepartment [{subdepartment.Name}] >>");
                    foreach (var product in subdepartment.Products)
                    {
                        Console.WriteLine(product.ToString());
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
