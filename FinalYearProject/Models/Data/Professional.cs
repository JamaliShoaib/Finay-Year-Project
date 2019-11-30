using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    [MetadataType(typeof(ProfessionalMetaData))]
    public partial class Professional
    {
        public string Prof_ConfirmPassword { get; set; }
        public string file { get; set; }
        public int Id { get; internal set; }
    }

    class ProfessionalMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string Prof_FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string Prof_LastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Prof_EmailID { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM//dd//yy}")]
        public DateTime Prof_DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required")]
        public string Prof_Address { get; set; }

        [Display(Name = "CNIC/Passport")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CNIC OR Passport is required")]

        public string Prof_CNIC { get; set; }

        [Display(Name = "Contact")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact is required")]
        public string Prof_PhoneNumber { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimium 6 characters is required")]
        [DataType(DataType.Password)]
        public string Prof_Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]

        [Compare("Prof_Password", ErrorMessage = "Password do not match")]
        public string Prof_ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string file { get; set; }

        public string Prof_img { get; set; }


    }



}
