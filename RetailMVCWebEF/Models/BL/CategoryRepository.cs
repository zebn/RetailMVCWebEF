using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.BL
{
    public class CategoryRepository
    {
        public static CategoryViewModel ViewModelFind(String id)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
            Product prod = db.Products.Find(id);
            if (prod != null)
                return null;
            else
                return null;
        }

        public static IQueryable<ProductViewModel> ViewModelListSet(/*int? RestaurantId*/)
        {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
        
           return DB.Products.Include(c => c.Restaurant).Select(c => new ProductViewModel()
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
            })/*.Where(c=>c.Restaurant.id == RestaurantId)*/.OrderBy(c => c.name);
        }
    }
}