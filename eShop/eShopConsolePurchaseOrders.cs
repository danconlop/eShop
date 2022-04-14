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

                // Obtener estatus primero para validar que no tenga status paid
                var poPreviousStatus = _purchaseOrderService.GetPurchaseOrderById(poId).Status;

                // Si ya está pagada, no se permiten cambios al status
                if (poPreviousStatus != PurchaseOrderStatus.Paid)
                {
                    var po = _purchaseOrderService.ChangeStatus(poId, newStatus);

                    if (newStatus == PurchaseOrderStatus.Paid)
                    {
                        // Actualizar el Stock de los productos originales que fueron comprados por la orden de compra que haya sido pagada
                        ActualizarStockDeProducto(po.PurchasedProducts);
                    }

                    Console.WriteLine("Orden de compra actualizada correctamente");

                } else
                {
                    Console.WriteLine("La orden ya está pagada, no se puede actualizar el estatus.");
                }
            } else
            {
                Console.WriteLine("El estatus solicitado no existe");
            }
            Console.ReadLine();

        }

        private void AgregarOrdenDeCompra()
        {
            PurchaseOrder purchaseOrder;
            List<Product> purchaseOrderProducts = new();
            string continuar;
            /*
             * ELEGIR PROVEEDOR
             */
            var ProvidersList = TestData.GetProviders();
            Console.WriteLine("Elije un proveedor");
            ProvidersList.ForEach(provider => Console.WriteLine($"{provider.Id}. {provider.Name}"));
            var providerIndex = ValidateInt(Console.ReadLine());
            var provider = ProvidersList.ElementAt(providerIndex - 1);
            /*
             * ELEGIR PRODUCTOS
             */
            do
            {
                // Mostrar lista de productos disponibles
                Console.WriteLine("Lista de productos disponibles");
                var productList = _productService.GetProducts();
                productList.ForEach(product => Console.WriteLine($"{product.Id}. {product.Name}"));

                Console.WriteLine("Selecciona el producto a agregar");
                var productIndex = ValidateInt(Console.ReadLine());
                var productData = productList.ElementAt(productIndex - 1);
                var product = new Product(productData.Id, productData.Name, productData.Price, productData.Description, productData.Brand, productData.Sku);
                Console.WriteLine("Ingrese la cantidad de producto");
                var productQuantity = ValidateInt(Console.ReadLine());
                // Se agrega el stock
                product.AddStock(productQuantity);
                // Se agrega el producto a la lista
                purchaseOrderProducts.Add(product);
                // Preguntar si se desea continuar agregando productos
                Console.WriteLine("Desea continuar agregando productos? (S/N)");
                continuar = Console.ReadLine();

                if (string.IsNullOrEmpty(continuar))
                    continuar = "N";

            } while (continuar.ToUpper() != "N");
            

            // Se agrega a la purchase order
            purchaseOrder = new PurchaseOrder(provider, purchaseOrderProducts);
            // Service para agregar la PO
            _purchaseOrderService.AddPurchaseOrder(purchaseOrder);
            Console.WriteLine("La orden de compra se agregó exitosamente");
            Console.ReadKey();
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
                _productService.UpdateStock(_productService.GetProduct(product.Id), product.Stock);
            }
        }
    }
}
