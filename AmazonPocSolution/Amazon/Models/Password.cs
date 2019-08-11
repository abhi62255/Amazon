using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [NotMapped]
    public partial class Password
    {

        public int Id { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Minmum length 6 character")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minmum length 6 character")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [CompareAttribute("NewPassword", ErrorMessage = "Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}