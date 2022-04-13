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
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. Top 5 de productos más caros ordenados por precio más alto");
            Console.WriteLine("2. Productos con 5 unidades o menos ordenados por unidades");
            Console.WriteLine("3. Nombre de productos por marcas ordenados por nombre");
            Console.WriteLine("4. Agrupación de departamentos con subdepartamentos y productos");
            Console.WriteLine("5. Regresar");

            switch (Console.ReadLine())
            {
                case "1": // Top 5 de productos más caros ordenados por precio más alto
                    //Reports.TopFive();
                    Top5ProductosMasCaros();
                    break;
                case "2": // Productos con 5 unidades o menos ordenados por unidades
                    //Reports.FiveUnitsOrdered();
                    FiveUnitsOrMore();
                    break;
                case "3": // Nombre de productos por marcas ordenados por nombre
                    //Reports.BrandOrderedByName();
                    //ProductsOrderedByName();
                    break;
                case "4": // Agrupación de departamentos con subdepartamentos y productos
                    //Reports.GroupByDepartmentSubdepartmentAndProduct();
                    ProductsGroupedByDepartment();
                    break;
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

        /*
        private void ProductsOrderedByName()
        {
            var data = _reportService.GetProductsOrderedByName();

            foreach(var dto in data)
            {
                Console.WriteLine($"{dto.Brand} {dto.Name}");
            }
        }
        */
        private void ProductsGroupedByDepartment()
        {
            /*
            var data = _reportService.GetProductsGroupedByDepartment();

            foreach(var dto in data)
            {
                Console.WriteLine($"{dto.Department} {dto.Subdepartment} {dto.Name}");
            }
            */
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
                Console.WriteLine($"Department {department.Name} >");
                var subdepartments = _departmentService.GetSubdepartments(department.Id);
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
