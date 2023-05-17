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
          
            ViewBagTables();
            ViewBagCategories();
            ViewBagProducts(searchProductByCodeOrName,restaurantId);
            ViewBagOrders(restaurantId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbrirOrdenEnMesa(int tableId, int restaurantId)
        {
            //Crea el objeto OrdenMesa
            OrderTbl orderTbl = new OrderTbl();
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
         
            ViewBagTables();
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(restaurantId, tableId);


            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewOrdersForTable(int tableId, int restaurantId)
        {
           
            ViewBagTables();
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(restaurantId, tableId);


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

            ViewBagTables();
            ViewBagCategories();
            ViewBagProducts("", restaurantId);
            ViewBagOrders(restaurantId, orderTbl.FK_id_idTable.Value);


            return View("Index");
        }

        void ViewBagTables()
        {
            ViewBag.TablesTPV = null;
            ViewBag.TablesTPV = TableRepository.ViewModelListSet().ToList();
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

        void ViewBagOrders(int restaurantId = 1, int table = -1)
        {
            ViewBag.currentTable = table;
            ViewBag.OrdersTPV = null;
            ViewBag.OrdersTPV = OrderRepository.ViewModelListSet(restaurantId, table).ToList();


        }

    }
}