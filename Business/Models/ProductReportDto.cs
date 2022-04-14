using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;

namespace Business.Models
{
    public class ProductReportDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public string Brand { get; set; }
    }

    public class ProductReportUnitDto
    {
        public string Name { get; set; }
        public int Units { get; set; }
    }

    public class ProductReportByBrandOrderedByName
    {
        public string Brand { get; set; }
        public List<ProductReportDto> Products { get; set; }
    }

    public class ProductReportGroupedByDepartment
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Subdepartment { get; set; }
    }

    /* PURCHASE ORDER DTO */
    public class POEstatusPagadoUltimos7DiasDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public PurchaseOrderStatus Status { get; set; }
    }

    public class POSillaDto
    {
        public int Id { get; set; }
        public List<Product> products = new List<Product>();
    }

    public class POEstatusPendienteDto
    {
        public int Id { get; set; }
        public Provider Provider { get; set; }
        public PurchaseOrderStatus Status { get; set; }
    }

}
