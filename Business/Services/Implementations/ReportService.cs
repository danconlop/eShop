using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Abstractions;
using Data.Entities;

namespace Business.Services.Implementations
{
    public class ReportService : IReportService
    {
        private List<Product> ProductList = TestData.ProductList;
        private List<Department> DepartmentList = TestData.DepartmentList;
        private DepartmentService _departmentService = new DepartmentService();

        public List<ProductReportDto> GetTop5PRoductosMasCaros()
        {
            return ProductList
                .OrderByDescending(c => c.Price)
                .Take(5)
                .Select(c => new ProductReportDto
                {
                    Name = c.Name,
                    Price = c.Price
                }).ToList();
        }

        public List<ProductReportUnitDto> Get5UnitsOrMoreOrderedByUnit()
        {
            return ProductList
                .Where(product => product.Stock <= 5)
                .OrderByDescending(product => product.Stock)
                .Select(product => new ProductReportUnitDto
                {
                    Name = product.Name,
                    Units = product.Stock
                }).ToList();
        }

        public List<ProductReportByBrandOrderedByName> GetProductsOrderedByName()
        {
            return ProductList
                .OrderBy(product => product.Name)
                .Select(product => new ProductReportDto { Name = product.Name, Brand = product.Brand })
                .GroupBy(product => product.Brand)
                .Select(group => new ProductReportByBrandOrderedByName { Brand = group.Key, Products = group.ToList() })
                .ToList();
        }

        /*
        public List<ProductReportGroupedByDepartment> GetProductsGroupedByDepartment()
        {
            //var departments = _departmentService.GetDepartments();

            //new ProductReportGroupedByDepartment
            //{
            //    foreach (var department in departments)
            //    {
            //        var subdepartments = _departmentService.GetSubdepartments(department.Id);
            //        foreach (var subdepartment in subdepartments)
            //        {
            //            foreach (var product in subdepartment.Products)
            //            {
            //                    Name = product.Name,
            //            }
            //        }
            //    }
            //}
        }
        */
        
    }
}
