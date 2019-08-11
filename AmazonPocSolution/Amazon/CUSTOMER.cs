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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            this.ADDRESSes = new HashSet<ADDRESS>();
            this.FEEDBACKs = new HashSet<FEEDBACK>();
            this.KARTs = new HashSet<KART>();
            this.ORDERPLACEDs = new HashSet<ORDERPLACED>();
            this.PREVISITs = new HashSet<PREVISIT>();
            this.SEARCHISTORies = new HashSet<SEARCHISTORY>();
            this.PRODUCTs = new HashSet<PRODUCT>();
        }


        public long CustomerId { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Maximun length 70 character")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(70, ErrorMessage = "Maximun length 70 character")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minmum length 6 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassowrd { get; set; }
        public Nullable<long> AddressId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        [Required]
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADDRESS> ADDRESSes { get; set; }
        public virtual ADDRESS ADDRESS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEEDBACK> FEEDBACKs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KART> KARTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDERPLACED> ORDERPLACEDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PREVISIT> PREVISITs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEARCHISTORY> SEARCHISTORies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }
    }
}
