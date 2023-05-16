using Microsoft.Ajax.Utilities;
using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;
using RetailMVCWebEF.Models.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class OrderRepository
    {

        public static OrderViewModel ViewModelFind(String id)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
            OrderTbl ord = db.OrderTbls.Find(id);
            if (ord != null)
                return new OrderViewModel().View(ord);
            else
                return null;
        }

        public static List<OrderViewModel> ViewModelListSet(int restaurantId = 1/*, int tableId = -1*/)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
            List<OrderViewModel> Orders = DB.OrderTbls.Select(c => new OrderViewModel()
            {
                id = c.id,
               isActive = c.isActive,
                isPaid = c.isPaid,
                creationTime = c.creationTime,
                FK_id_idTable = c.FK_id_idTable,
                FK_id_idRestaurant = restaurantId
            }).Where(c => c.isActive == true).OrderBy(c => c.creationTime).ToList();
            Orders = Orders == null ? new List<OrderViewModel>() : Orders;

           // Orders = Orders.Where(c => tableId == -1 || c.FK_id_idTable == tableId).ToList();

            for (int i = 0; i < Orders.Count(); i++)
            {
                List<ProductOrderDetailViewModel> ProductOrders = ProductOrderDetaillRepository.ViewModelListSet(Orders.ToList().ElementAt(i).id).ToList();

                for (int j = 0; j < ProductOrders.Count(); j++)
                {
                    if(ProductOrders.ElementAt(j).FK_id_idProduct != null) // si el producto existe en la orden
                    ProductOrders.ElementAt(j).Product = ProductRepository.ViewModelFind(ProductOrders.ElementAt(j).FK_id_idProduct.Value);
                }

               TableViewModel TableOrder = TableRepository.ViewModelFind(Orders.ElementAt(i).FK_id_idTable.Value); // mesa de la orden i
                Orders.ElementAt(i).TableTbl = TableOrder;

                for (int j = 0; j < ProductOrders.Count(); j++)
                {
                    if (ProductOrders.ElementAt(j).FK_id_idProduct != null) // si el producto existe en la orden
                        ProductOrders.ElementAt(j).Product = ProductRepository.ViewModelFind(ProductOrders.ElementAt(j).FK_id_idProduct.Value);
                }

                Orders.ElementAt(i).ProductOrderDetails = ProductOrders;
            }

            return Orders;
        }

    }
}