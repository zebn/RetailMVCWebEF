using RetailMVCWebEF.Models.BL;
using RetailMVCWebEF.Models.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetailMVCWebEF.Models.VL;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity;

namespace RetailMVCWebEF.Controllers
{
    public class TPVController : Controller
    {
        GestionHosteleriaGenNHibernateEntities1 db = new GestionHosteleriaGenNHibernateEntities1();
        // GET: TPV
        public ActionResult Index(string searchProductByCodeOrName = "", int restaurantId = 1)
        {
          
            ViewBagTables(1);
            ViewBagCategories();
            ViewBagProducts(searchProductByCodeOrName,restaurantId);
            ViewBagOrders(1);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbrirOrdenEnMesa(int tableId, int restaurantId)
        {

            var orderList = OrderRepository.GetActiveOrders();

            if (orderList.Count < TableRepository.ViewModelListSet().ToList().Count)
            {
                //Crea el objeto OrdenMesa
                var orderTbl = new OrderTbl();
                orderTbl.FK_id_idTable = tableId;
                orderTbl.FK_id_idRestaurant = restaurantId;
                orderTbl.isPaid = false;
                orderTbl.isActive = true;
                orderTbl.creationTime = DateTime.Now;
                orderTbl.total = 0;

                //Inserta el objeto OrdenMesa
                db.OrderTbls.Add(orderTbl);
           
                db.Entry(orderTbl).State = EntityState.Added;
           
                db.SaveChanges();
            }

            ViewBagTables(tableId);
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(tableId);
            
            


            return View("Index");
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewOrdersForTable(int tableId, int restaurantId)
        {
           
            ViewBagTables(tableId);
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(tableId);


            return View("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addEntry(int tableId, int orderId, int quantity, int productId)
        {
            var orderDetailViewModelList = ProductOrderDetaillRepository.ViewModelListSet(orderId);

            var entry = orderDetailViewModelList.FirstOrDefault(x => x.FK_id_idProduct == productId);

            if (entry == null)
            {
                //Adds entry to the product
                ProductOrderDetail newEntry = new ProductOrderDetail();
                newEntry.FK_id_idProduct = productId;
                newEntry.FK_id_idOrders = orderId;
                newEntry.quantity = quantity;

                db.ProductOrderDetails.Add(newEntry);
                db.SaveChanges();
            }
            else
            {
          
                ProductOrderDetail entryML = db.ProductOrderDetails.Find(entry.id);
                entryML.quantity = entryML.quantity + quantity;
                
                db.ProductOrderDetails.Attach(entryML);
                db.Entry(entryML).State = EntityState.Modified;
                db.SaveChanges();
            }

            /*var order = OrderRepository.ViewModelFind(orderId);
            order.total = 0;
            foreach (var item in order.ProductOrderDetails)
            {
                var subtotal = item.quantity * item.Product.price;
                order.total = order.total + subtotal;
            }

            var orderTbl = db.OrderTbls.Find(orderId);
            orderTbl.total = order.total;
            db.OrderTbls.Add(orderTbl);
            db.SaveChanges();*/
         
            ViewBagTables(tableId);
            ViewBagCategories();
            ViewBagProducts("", 1);
            ViewBagOrders(tableId);


            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseOrder(int orderId, int restaurantId)
        {
            ////Crea el objeto OrdenMesa
            //OrderTbl orderTbl = new OrderTbl();
            //orderTbl.FK_id_idTable = tableId;
            //orderTbl.FK_id_idRestaurant = restaurantId;
            //orderTbl.isPaid = false;
            //orderTbl.isActive = true;
            //orderTbl.creationTime = DateTime.Now;
            //orderTbl.total = 0;

            ////Inserta el objeto OrdenMesa
            //db.OrderTbls.Add(orderTbl);

            OrderTbl orderTbl = db.OrderTbls.Find(orderId);

            orderTbl.isActive=false;
            orderTbl.isPaid = true;

            db.OrderTbls.Attach(orderTbl);

            

            db.Entry(orderTbl).State = EntityState.Modified;
            db.SaveChanges();

            //db.SaveChanges();

            ViewBagTables(1);
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(orderTbl.FK_id_idTable.Value);


            return View("Index");
        }

        void ViewBagTables(int currentTableId)
        {
            ViewBag.TablesTPV = null;
            var tableList = TableRepository.ViewModelListSet().ToList();
            var currentTable = tableList.FirstOrDefault(x => x.id == currentTableId);
            currentTable.isActive = true;
            ViewBag.TablesTPV = tableList;
        }

        void ViewBagProducts(string searchProductByCodeOrName = "", int restaurantId = 1)
        {
            ViewBag.ProductsTPV = null;
            ViewBag.ProductsTPV = ProductRepository.ViewModelListSet(searchProductByCodeOrName, restaurantId).ToList();
        }

        void ViewBagCategories()
        {
            var enumList = Enum.GetValues(typeof(Enums.Categories))
       .Cast<Enums.Categories>()
       .Select(e => new SelectListItem() { Value = ((int)e).ToString(), Text = e.ToString() })
       .ToList();

            ViewBag.CategoriesTPV = enumList;
        }

        void ViewBagOrders(int tableId)
        {
            ViewBag.currentTable = tableId;
            ViewBag.OrdersTPV = null;
            ViewBag.OrdersTPV = OrderRepository.ViewModelListSet(1, tableId).ToList();

            /*
            var orderList = OrderRepository.ViewModelListSet(1).ToList();
            var currentOrder = orderList.FirstOrDefault(x => x.FK_id_idTable == tableId);
            if (currentOrder != null)
            {
                currentOrder.isCurrent = true;
            }
            ViewBag.OrdersTPV = orderList;
            */

        }

    }
}