using RetailMVCWebEF.Models.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.VL
{
    public class ProductViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductViewModel()
        {
            this.ProductOrderDetails = new HashSet<ProductOrderDetail>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public string description { get; set; }
        public string allergens { get; set; }
        public string nutritionFacts { get; set; }
        public float price { get; set; }
        public string imageUrl { get; set; }
        public string code { get; set; }
        public Nullable<int> FK_id_idRestaurant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrderDetail> ProductOrderDetails { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public Product Model(Product model)
        {
            if (model != null)
            {
                model.id=this.id;
                model.name = this.name;
                model.isActive = this.isActive;
                model.description = this.description;
                model.allergens = this.allergens;
                model.nutritionFacts = this.nutritionFacts;
                model.price = this.price;
                model.imageUrl = this.imageUrl;
                model.code = this.code;
            }
            return model;

        }
        public ProductViewModel View(Product model, Boolean GetInstanceIfIsNull = true)
        {
            if(model != null)
            {
                this.id =model.id;
                this.name = model.name;
                this.isActive = model.isActive; 
                this.description = model.description;
                this.allergens = model.allergens;
                this.nutritionFacts = model.nutritionFacts;
                this.price = model.price;
                this.imageUrl = model.imageUrl;
                this.code = model.code;
                return this;
            }
            else if (GetInstanceIfIsNull)
                return this;
            else
                return null;

        }

        public Product Model()
        {
            return this.Model(new Product());
        }

        public ProductViewModel View()
        {
            return this.View(new Product());
        }
    }
}