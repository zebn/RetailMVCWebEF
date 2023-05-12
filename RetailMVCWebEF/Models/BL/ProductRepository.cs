using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class ProductRepository
    {
        public static ProductViewModel ViewModelFind(int id)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
            Product prod = db.Products.Find(id);
            if (prod != null)
                return new ProductViewModel().View(prod);
            else
                return null;
        }

        public static IQueryable<ProductViewModel> ViewModelListSet(string searchProductByCodeOrName,int RestaurantId=1)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
        
           return DB.Products.Select(c => new ProductViewModel()
            {
               id = c.id,
               name = c.name,
               isActive = c.isActive,
               description = c.description,
               allergens = c.allergens,
               nutritionFacts   = c.nutritionFacts,
               price = c.price,
               imageUrl = c.imageUrl,
               code = c.code,
            }).Where(c=> RestaurantId == 1 /*|| c.FK_id_idRestaurant == RestaurantId*/&&(
            String.IsNullOrEmpty(searchProductByCodeOrName) || 
            c.code.Contains(searchProductByCodeOrName) || c.name.Contains(searchProductByCodeOrName))).OrderBy(c => c.name);
        }
    }
}