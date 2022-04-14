using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business.Services.Implementations;
using Business;
using Data.Enums;
using System.Globalization;

namespace eShop
{
    public partial class eShopConsole
    {
        
        private List<Product> ProductList = TestData.ProductList;
        
        private bool MenuDeOrdenesDeCompra()
        {
            Console.WriteLine("Elije una opcion:");
            Console.WriteLine("1. Agregar orden de compra");
            Console.WriteLine("2. Mostrar ordenes de compra");
            Console.WriteLine("3. Cambiar estatus");
            Console.WriteLine("4. Regresar");

            switch (Console.ReadLine())
            {
                case "1":
                    AgregarOrdenDeCompra();
                    break;
                case "2":
                    MostrarOrdenDeCompra();
                    break;
                case "3":
                    CambiarEstatusOrdenCompra();
                    break;
                case "4":
                default:
                    return false;
            }

            return true;
        }

        private void CambiarEstatusOrdenCompra()
        {
            Console.WriteLine("A que orden quieres cambiarle el estatus?");
            var InpoId = Console.ReadLine();
            Int32.TryParse(InpoId, out int poId);

            Console.WriteLine("A que estatus quieres cambiarlo?");
            foreach(var status in Enum.GetNames<PurchaseOrderStatus>())
            {
                Console.WriteLine(status);
            }
            //var statusAux = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
            var statusAux = Console.ReadLine();

            PurchaseOrderStatus newStatus;
            var didParse = Enum.TryParse(statusAux, out newStatus);

            if (didParse)
            {

                // Obtener estatus primero para validar que no tenga ya paid
                var poPreviousData = _purchaseOrderService.GetPurchaseOrderById(poId);
                var po = _purchaseOrderService.ChangeStatus(poId, newStatus);
                if (newStatus == PurchaseOrderStatus.Paid)
                {
                    // Actualizar el Stock de los productos originales que fueron comprados por la orden de compra que haya sido pagada
                    if (poPreviousData.Status != PurchaseOrderStatus.Paid && po.Status == PurchaseOrderStatus.Paid)
                    {
                        ActualizarStockDeProducto(po.PurchasedProducts);
                    }
                }
                Console.WriteLine("Orden de compra actualizada correctamente");
            } else
            {
                Console.WriteLine("El estatus solicitado no existe");
            }
            Console.ReadLine();

        }

        // TODO: Unificar ObtenerProveedor y ObtenerListaDeProductos dentro de AgregarOrdenDeCompra
        private void AgregarOrdenDeCompra()
        {
            PurchaseOrder purchaseOrder;

            Console.WriteLine("Elije un proveedor");
            var provider = ObtenerProveedor();
            Console.WriteLine("Lista de productos disponibles");
            var purchaseOrderProducts = ObtenerListaDeProductos();
            // Se agrega a la purchase order
            purchaseOrder = new PurchaseOrder(provider, purchaseOrderProducts);
            // Service para agregar la PO
            _purchaseOrderService.AddPurchaseOrder(purchaseOrder);
            Console.WriteLine("La orden de compra se agregó exitosamente");
            Console.ReadKey();
        }
        private Provider ObtenerProveedor()
        {
            var ProvidersList = TestData.GetProviders();
            foreach (var provider in ProvidersList)
            {
                Console.WriteLine($"{provider.Id}. {provider.Name}");
            }
            var providerIndex = Console.ReadLine();
            Int32.TryParse(providerIndex, out int providerIndexAux);
            return ProvidersList.ElementAt(providerIndexAux - 1);
        }

        private List<Product> ObtenerListaDeProductos()
        {
            List<Product> PurchaseOrderProducts = new List<Product>();
            string continuar;

            foreach (var product in ProductList)
            {
                Console.WriteLine($"{product.Id}. {product.Name}");
            }

            do
            {
                Console.WriteLine("Selecciona el producto a agregar");
                var productIndex = Console.ReadLine();
                Int32.TryParse(productIndex, out int productIndexAux);
                var productSelected = _productService.GetProduct(productIndexAux);
                var productSelectedAux = new Product(productSelected.Id, productSelected.Name, productSelected.Price, productSelected.Description, productSelected.Brand, productSelected.Sku, productSelected.Stock);
                Console.WriteLine("Ingrese la cantidad de producto");
                var productQuantity = Console.ReadLine();
                Int32.TryParse(productQuantity, out int productQuantityAux);
                // Se agrega el stock
                productSelectedAux.AddStock(productQuantityAux);
                // Se agrega el producto a la lista
                PurchaseOrderProducts.Add(productSelectedAux);
                Console.WriteLine("Desea continuar agregando productos? (S/N)");
                continuar = Console.ReadLine();

                if (string.IsNullOrEmpty(continuar))
                    continuar = "N";

            } while (continuar.ToUpper() != "N");

            return PurchaseOrderProducts;
        }

        private void MostrarOrdenDeCompra()
        {
            int orderCount = 1;
            if (_purchaseOrderService.GetPurchaseOrders().Any())
            {
                foreach(var orden in _purchaseOrderService.GetPurchaseOrders())
                {
                    Console.WriteLine($"----------- ORDEN [{orderCount}]");
                    Console.WriteLine(orden.ToString()); ;
                    orderCount++;
                }
            } else
            {
                Console.WriteLine("No existen ordenes de compra");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarStockDeProducto(List<Product> purchaseOrder)
        {
            foreach(var product in purchaseOrder)
            {
                _productService.UpdateStock(product, product.Stock);
            }
        }
    }
}
