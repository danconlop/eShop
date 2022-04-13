using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



}
