using RetailMVCWebEF.Models.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailMVCWebEF.Models.VL
{
    public class TableViewModel
    {
        public TableViewModel()
        {
            this.Books = new HashSet<Book>();
            this.OrderTbls = new HashSet<OrderTbl>();
        }

        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isAvailable { get; set; }
        public int code { get; set; }
        public int capacity { get; set; }
        public Nullable<int> FK_id_idRestaurant { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public TableTbl Model(TableTbl model)
        {
            if (model != null)
            {
                model.id=this.id;
                model.isAvailable = this.isAvailable;
                model.code = this.code;
                model.capacity = this.capacity;
            }
            return model;

        }
        public TableViewModel View(TableTbl model, Boolean GetInstanceIfIsNull = true)
        {
            if(model != null)
            {
                this.id =model.id;
                this.isAvailable = model.isAvailable;
                this.code = model.code; 
                this.capacity = model.capacity;
                return this;
            }
            else if (GetInstanceIfIsNull)
                return this;
            else
                return null;

        }

        public TableTbl Model()
        {
            return this.Model(new TableTbl());
        }

        public TableViewModel View()
        {
            return this.View(new TableTbl());
        }
    }
}