//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetailMVCWebEF.Models.ML
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public int idBook { get; set; }
        public int clientNumber { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public System.DateTime time { get; set; }
        public Nullable<int> FK_id_idTableTbl { get; set; }
        public Nullable<int> FK_id_idRestaurant { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
        public virtual TableTbl TableTbl { get; set; }
    }
}
