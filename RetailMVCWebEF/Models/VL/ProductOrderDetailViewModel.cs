using RetailMVCWebEF.Models.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.VL
{
    public class ProductOrderDetailViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOrderDetailViewModel()
        {
           
        }

        public int id { get; set; }
        public int quantity { get; set; }
        public Nullable<int> FK_id_idProduct { get; set; }
        public Nullable<int> FK_id_idOrders { get; set; }

        public virtual OrderTbl OrderTbl { get; set; }
        public virtual ProductViewModel Product { get; set; }

        public ProductOrderDetail Model(ProductOrderDetail model)
        {
            if (model != null)
            {
                model.id=this.id;
                model.quantity = this.quantity;
                model.FK_id_idProduct = this.FK_id_idProduct;
                model.FK_id_idOrders = this.FK_id_idOrders;
              
            }
            return model;

        }
        public ProductOrderDetailViewModel View(ProductOrderDetail model, Boolean GetInstanceIfIsNull = true)
        {
            if(model != null)
            {
                this.id =model.id;
                this.quantity = model.quantity;
                this.FK_id_idProduct = model.FK_id_idProduct; 
                this.FK_id_idOrders = model.FK_id_idOrders;
               
                return this;
            }
            else if (GetInstanceIfIsNull)
                return this;
            else
                return null;

        }

        public ProductOrderDetail Model()
        {
            return this.Model(new ProductOrderDetail());
        }

        public ProductOrderDetailViewModel View()
        {
            return this.View(new ProductOrderDetail());
        }
    }
}