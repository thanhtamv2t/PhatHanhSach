//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhatHanhSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class agent_payment_detail
    {
        public int id { get; set; }
        public int id_payment { get; set; }
        public int id_book { get; set; }
        public int quantity { get; set; }
    
        public virtual agent_payment agent_payment { get; set; }
        public virtual book book { get; set; }
    }
}