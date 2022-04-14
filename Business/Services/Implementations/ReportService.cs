using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Business.Services.Abstractions;
using Data.Entities;
using Data.Enums;

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

        public List<POEstatusPagadoUltimos7DiasDto> GetPOEstatusPagadoUltimos7Dias(List<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders
                .Where(po => po.PurchaseDate.Subtract(DateTime.Now).Days <= 7 && po.Status == PurchaseOrderStatus.Paid)
                .Select(po => new POEstatusPagadoUltimos7DiasDto
                {
                    Id = po.Id,
                    PurchaseDate = po.PurchaseDate,
                    Status = po.Status
                }).ToList();
        }

        public List<POSillaDto> GetPOSillas(List<PurchaseOrder> purchaseOrders)
        {
            //throw new NotImplementedException();
            var r = purchaseOrders
                .GroupBy(po => po.PurchasedProducts.FindAll(product => product.Name.Equals("Silla")))
                .Select(group => new POSillaDto
                {
                    //Id = group.Key,

                }).ToList();
        }

        public List<POEstatusPendienteDto> GetPOEstatusPendientes(List<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders
                .Where(po => po.Status == PurchaseOrderStatus.Pending && po.Provider.Name.Equals("Levis"))
                .Select(po => new POEstatusPendienteDto
                {
                    Id = po.Id,
                    Status = po.Status,
                    Provider = po.Provider
                }).ToList();
        }

        public List<ProductoMasUnidadesCompradasDto> GetProductoMasUnidadesCompradas(List<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders
                .GroupBy(po => po.PurchasedProducts.Sum(product => product.Stock))
                .Select(group => group.Max())
                .Select(group => new ProductoMasUnidadesCompradasDto
                {
                    OrderId = group.Id,
                    OrderDate = group.PurchaseDate,
                    //ProductName = group.PurchasedProducts
                    //Quantity = group
                }).ToList();
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
