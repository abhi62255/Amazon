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
    
    public partial class PRODUCTDESCRIPTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTDESCRIPTION()
        {
            this.PRODUCTs = new HashSet<PRODUCT>();
        }
    
        public long DescriptionId { get; set; }
        public long ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductGenderType { get; set; }
        public string ProductBrand { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductDescription1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
