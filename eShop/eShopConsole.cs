using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Abstractions;
using Business.Services.Implementations;
using Data.Entities;

namespace eShop
{
    public partial class eShopConsole
    {
        private readonly IProductService _productService;
        private readonly IDepartmentService _departmentService;
        private readonly IReportService _reportService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ICartService _cartService;

        public eShopConsole()
        {
            _productService = new ProductService();
            _departmentService = new DepartmentService();
            _reportService = new ReportService();
            _purchaseOrderService = new PurchaseOrderService();
            _cartService = new CartService();
        }


        // Menú PRINCIPAL
        public bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. CLIENTE");
            Console.WriteLine("2. ADMINISTRADOR");
            Console.WriteLine("3. Salir del sistema");

            switch (Console.ReadLine())
            {
                case "1": // Cliente
                    bool showClientMenu = true;
                    while (showClientMenu)
                    {
                        showClientMenu = MenuCliente();
                    }
                    break;
                case "2": // Administrador
                    bool showAdminMenu = true;
                    while (showAdminMenu)
                    {
                        showAdminMenu = MenuAdmin();
                    }
                    break;
                case "3":
                default:
                    return false;
            }

            return true;
        }

        // Menú ADMINISTRADOR
        public bool MenuAdmin()
        {
            Console.Clear();
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Editar producto");
            Console.WriteLine("3. Lista de productos");
            Console.WriteLine("4. Consultar producto");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Reportes");
            Console.WriteLine("7. Ordenes de compra");
            Console.WriteLine("8. Regresar");

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
                    bool showReportMenu = true;
                    while (showReportMenu)
                    {
                        showReportMenu = MenuDeReportes();
                    }
                    break;
                case "7":
                    bool showPOMenu = true;
                    while (showPOMenu)
                    {
                        showPOMenu = MenuDeOrdenesDeCompra();
                    }
                    break;
                case "8":
                default:
                    return false;
            }

            return true;
        }

        // Menú CLIENTE
        public bool MenuCliente()
        {
            Console.Clear();
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("----------- CARRITO -----------");
            Console.WriteLine("1. Agregar productos al carrito");
            Console.WriteLine("2. Mostrar productos en el carrito");
            Console.WriteLine("3. Editar carrito");
            Console.WriteLine("4. Cancelar compra");
            Console.WriteLine("----------- PEDIDOS -----------");
            Console.WriteLine("5. Realizar pedido");
            Console.WriteLine("6. Consultar pedidos");
            Console.WriteLine("7. Regresar");

            switch (Console.ReadLine())
            {
                case "1": // Agregar productos al carrito
                    CartAddProducts();
                    break;
                case "2": // Editar carrito
                    CartShowDetail();
                    break;
                case "3": // Cancelar compra
                    
                    break;
                case "4": // Realizar pedido

                    break;
                case "5":

                    break;
                case "6": // Consultar pedidos realizados (total gastado por el cliente)

                    break;
                case "7":
                default:
                    return false;
            }

            return true;
        }
        private void AgregarProducto()
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

        private void MostrarProductos()
        {
            Console.Clear();
            foreach (var product in _productService.GetProducts())
            {
                Console.WriteLine(product.ToString());
            }
        }

        private void ConsultarProducto()
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

        private void EditarProducto()
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

        private void EliminarProducto()
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

        private void MostrarDepartamentos()
        {
            foreach (var department in _departmentService.GetDepartments())
            {
                Console.WriteLine($"{department.Id}. {department.Name}");
            }
        }

        private Subdepartment SolicitarSubdepartamento()
        {
            Console.WriteLine("Elije el departamento");
            var departments = _departmentService.GetDepartments();
            foreach (var element in departments)
            {
                Console.WriteLine($"{element.Id}. {element.Name}");
            }
            var departmentIndex = ValidateInt(Console.ReadLine()) - 1;
            var department = departments.ElementAt(departmentIndex);
            // Subdepartments
            Console.WriteLine($"Elija el subdepartamento de {departments.ElementAt(departmentIndex).Name}");
            foreach (var element in department.Subdepartments)
            {
                Console.WriteLine($"{element.Id}. {element.Name}");
            }
            var subdepartmentIndex = ValidateInt(Console.ReadLine()) - 1;
            var subdepartment = department.Subdepartments.ElementAt(subdepartmentIndex);
            subdepartment.Department = department;

            return subdepartment;
        }

        public int ValidateInt(string input)
        {
            if (!Int32.TryParse(input, out int outputInt))
            {
                throw new InvalidCastException("Debe ingresar un número");
            }

            return outputInt;
        }
    }
}
