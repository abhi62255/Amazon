//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Amazon
{
    using System;
    using System.Collections.Generic;
    
    public partial class PREVISIT
    {
        public long ProductId { get; set; }
        public long CustomerId { get; set; }
        public System.DateTime PreVisitDate { get; set; }
    
        public virtual CUSTOMER CUSTOMER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
