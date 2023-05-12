using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class TableRepository
    {
        public static TableViewModel ViewModelFind(int id)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
            TableTbl prod = db.TableTbls.Find(id);
            if (prod != null)
                return new TableViewModel().View(prod);
            else
                return null;
        }

        public static IQueryable<TableViewModel> ViewModelListSet(/*int? RestaurantId*/)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
      var Tables = DB.TableTbls.Include(c => c.Restaurant).Select(c => new TableViewModel()
      {
          id = c.id,
          isAvailable = c.isAvailable,
          code = c.code,
          capacity = c.capacity
      })/*.Where(c=>c.Restaurant.id == RestaurantId)*/.OrderBy(c => c.code);
            return Tables;
        }
    }
}