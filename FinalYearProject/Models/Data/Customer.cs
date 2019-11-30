using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        public string ConfirmPassword { get; set; }
    }

    class CustomerMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM//dd//yy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required")]
        public string Address { get; set; }

        [Display(Name = "CNIC/Passport")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CNIC OR Passport is required")]

        public string CNIC { get; set; }

        [Display(Name = "Contact")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact is required")]
        public string Contact { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimium 6 characters is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]

        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }


    }



}
