using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business.Models;

namespace Business.Services.Abstractions
{
    public interface IReportService
    {
        public List<ProductReportDto> GetTop5PRoductosMasCaros();
        public List<ProductReportUnitDto> Get5UnitsOrMoreOrderedByUnit();
        public List<ProductReportByBrandOrderedByName> GetProductsOrderedByName();
        //public List<ProductReportGroupedByDepartment> GetProductsGroupedByDepartment();

        public List<POEstatusPagadoUltimos7DiasDto> GetPOEstatusPagadoUltimos7Dias();

        public List<POSillaDto> GetPOSillas();
        public List<POEstatusPendienteDto> GetPOEstatusPendientes();
        public Product GetProductoMasUnidadesCompradas(); 
    }
}
