using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business.Services.Implementations;
using Business;

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
            Console.WriteLine("3. Regresar");

            switch (Console.ReadLine())
            {
                case "1":
                    AgregarOrdenDeCompra();
                    break;
                case "2":
                    MostrarOrdenDeCompra();
                    break;
                case "3":
                    break;
                default:
                    return false;
            }

            return true;
        }

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
                var productSelected = ProductList.ElementAt(productIndexAux - 1);
                Console.WriteLine("Ingrese la cantidad de producto");
                var productQuantity = Console.ReadLine();
                Int32.TryParse(productQuantity, out int productQuantityAux);
                // Se agrega el stock
                productSelected.AddStock(productQuantityAux);
                // Se agrega el producto a la lista
                PurchaseOrderProducts.Add(productSelected);
                Console.WriteLine("Desea continuar agregando productos? (Y/N)");
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
    }
}
