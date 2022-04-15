using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Implementations;
using Data.Entities;

namespace eShop
{
    public partial class eShopConsole
    {
        private bool MenuDeReportes()
        {
            Console.WriteLine("Elije una opción:");
            Console.WriteLine("1. Top 5 de productos más caros ordenados por precio más alto");
            Console.WriteLine("2. Productos con 5 unidades o menos ordenados por unidades");
            Console.WriteLine("3. Nombre de productos por marcas ordenados por nombre");
            Console.WriteLine("4. Agrupación de departamentos con subdepartamentos y productos");
            Console.WriteLine("------------ REPORTES DE ORDENES DE COMPRA --------------------------");
            Console.WriteLine("5. Estatus PAGADO en los últimos 7 días");
            Console.WriteLine("6. PO que incluyan silla entre sus productos");
            Console.WriteLine("7. Estatus PENDIENTE proveedor LEVIS");
            Console.WriteLine("8. Producto con más unidades compradas");
            Console.WriteLine("9. Regresar");

            switch (Console.ReadLine())
            {
                case "1": // Top 5 de productos más caros ordenados por precio más alto
                    Top5ProductosMasCaros();
                    break;
                case "2": // Productos con 5 unidades o menos ordenados por unidades
                    FiveUnitsOrMore();
                    break;
                case "3": // Nombre de productos por marcas ordenados por nombre
                    ProductsOrderedByName();
                    break;
                case "4": // Agrupación de departamentos con subdepartamentos y productos
                    ProductsGroupedByDepartment();
                    break;
                case "5": // Estatus PAGADO ultimos 7 días
                    EstatusPagadoUltimos7Dias();
                    break;
                case "6": //PO Que incluya silla entre sus productos
                    SillaEntreSusProductos();
                    break;
                case "7": // Estatus PENDIENTE proveedor LEVIS
                    EstatusPendiente();
                    break;
                case "8": // Producto mas comprado
                    ProductoMasComprado();
                    break;
                case "9":
                default:
                    return false;
            }

            return true;
        }

        private void Top5ProductosMasCaros()
        {
            var data = _reportService.GetTop5PRoductosMasCaros();

            foreach(var dto in data)
            {
                Console.WriteLine($"{dto.Name} {dto.Price}");
            }
        }

        private void FiveUnitsOrMore()
        {
            var data = _reportService.Get5UnitsOrMoreOrderedByUnit();

            foreach(var dto in data)
            {
                Console.WriteLine($"{dto.Name} {dto.Units}");
            }
        }
        
        private void ProductsOrderedByName()
        {
            //Agrupación de departamentos con subdepartamentos y productos
            var departments = _departmentService.GetDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine($"Departamento {department.Name} >");
                var subdepartments = _departmentService.GetSubdepartments(department.Id);
                foreach (var subdepartment in subdepartments)
                {
                    Console.WriteLine($"Subdepartmento [{subdepartment.Name}] >>");
                    foreach (var product in subdepartment.Products)
                    {
                        Console.WriteLine(product.ToString());
                    }
                }
            }

            Console.ReadKey();
        }
        
        private void ProductsGroupedByDepartment()
        {
            var result = _productService.GetProducts()
                .GroupBy(p => new { Department = p.Subdepartment.Department, Subdepartment = p.Subdepartment.Name })
                .GroupBy(group => group.Key.Department);

            foreach (var department in result)
            {
                Console.WriteLine($"- Departmento: {department.Key}");
                foreach (var subdepartment in department)
                {
                    Console.WriteLine($"\t- Subdepartmento: {subdepartment.Key.Subdepartment}");
                    foreach (var product in subdepartment)
                    {
                        Console.WriteLine($"\t\t- {product.Name}");
                    }
                }
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadLine();
        }

        private void EstatusPagadoUltimos7Dias()
        {
            var data = _reportService.GetPOEstatusPagadoUltimos7Dias();

            foreach (var dto in data)
            {
                Console.WriteLine($"ID {dto.Id} \nFecha de compra {dto.PurchaseDate}\nEstatus {dto.Status}");
            }
        }

        private void SillaEntreSusProductos()
        {
            var data = _reportService.GetPOSillas();

            foreach (var dto in data)
            {
                Console.WriteLine($"ID {dto.Id}");
                foreach(var prod in dto.products)
                {
                    Console.WriteLine($"Nombre de producto {prod.Name}\nDescripción {prod.Description}");
                }
            }
        }

        private void EstatusPendiente()
        {
            var data = _reportService.GetPOEstatusPendientes();

            foreach(var dto in data)
            {
                Console.WriteLine($"ID {dto.Id}\tProveedor {dto.Provider}\tEstatus {dto.Status}");
            }
        }

        private void ProductoMasComprado()
        {
            var data = _reportService.GetProductoMasUnidadesCompradas();

            Console.WriteLine($"El producto más comprado es\t{data.Name.ToUpper()}");
        }
    }

    public class Reports
    {
        private static ProductService _productService = new ProductService();
        private static DepartmentService _departmentService = new DepartmentService();
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
            var departments = _departmentService.GetDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine($"Departmento {department.Name} >");
                var subdepartments = _departmentService.GetSubdepartments(department.Id);
                foreach (var subdepartment in subdepartments)
                {
                    Console.WriteLine($"Subdepartmento [{subdepartment.Name}] >>");
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
