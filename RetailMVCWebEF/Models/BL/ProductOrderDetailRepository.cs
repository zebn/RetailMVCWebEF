using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class ProductOrderDetaillRepository
    {

        public static ProductOrderDetailViewModel ViewModelFind(String id)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
            ProductOrderDetail prodOrderDetail = db.ProductOrderDetails.Find(id);
            if (prodOrderDetail != null)
                return new ProductOrderDetailViewModel().View(prodOrderDetail);
            else
                return null;
        }

        public static IQueryable<ProductOrderDetailViewModel> ViewModelListSet(int orderId= 1)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
            var ProductOrderDetails = DB.ProductOrderDetails.Select(c => new ProductOrderDetailViewModel()
            {
                id = c.id,
              FK_id_idOrders = c.FK_id_idOrders,
              FK_id_idProduct = c.FK_id_idProduct,
              quantity = c.quantity
            }).Where(c=>c.FK_id_idOrders == orderId);
            return ProductOrderDetails;
        }

    }
}