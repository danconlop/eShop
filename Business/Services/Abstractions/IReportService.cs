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

        public List<POEstatusPagadoUltimos7DiasDto> GetPOEstatusPagadoUltimos7Dias(List<PurchaseOrder> purchaseOrders);

        public List<POSillaDto> GetPOSillas(List<PurchaseOrder> purchaseOrders);
        public List<POEstatusPendienteDto> GetPOEstatusPendientes(List<PurchaseOrder> purchaseOrders);
        public List<ProductoMasUnidadesCompradasDto> GetProductoMasUnidadesCompradas(List<PurchaseOrder> purchaseOrders); 
    }
}
