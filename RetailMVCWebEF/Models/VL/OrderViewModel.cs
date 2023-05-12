using RetailMVCWebEF.Models.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.VL
{
    public class OrderViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderViewModel()
        {
            this.ProductOrderDetails = new HashSet<ProductOrderDetailViewModel>();
        }

        public int id { get; set; }
        public System.DateTime creationTime { get; set; }
        public bool isActive { get; set; }
        public double total { get; set; }
        public bool isPaid { get; set; }
        public Nullable<int> FK_id_idTable { get; set; }
        public Nullable<int> FK_id_idRestaurant { get; set; }

        public virtual TableViewModel TableTbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrderDetailViewModel> ProductOrderDetails { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public OrderTbl Model(OrderTbl model)
        {
            if (model != null)
            {
                model.id=this.id;
                model.creationTime = this.creationTime;
                model.isActive = this.isActive;
                model.total = this.total;
                model.isPaid = this.isPaid;
              
            }
            return model;

        }
        public OrderViewModel View(OrderTbl model, Boolean GetInstanceIfIsNull = true)
        {
            if(model != null)
            {
                this.id =model.id;
                this.creationTime = model.creationTime;
                this.isActive = model.isActive; 
                this.total = model.total;
                this.isPaid = model.isPaid;
                return this;
            }
            else if (GetInstanceIfIsNull)
                return this;
            else
                return null;

        }

        public OrderTbl Model()
        {
            return this.Model(new OrderTbl());
        }

        public OrderViewModel View()
        {
            return this.View(new OrderTbl());
        }
    }
}