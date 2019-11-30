using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class ProfessionalLogin

    {
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Id Required")]
        public string Prof_EmailID { get; set; }

        [Display(Name ="Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Prof_Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }



        public int id { get; set; }


    }
}