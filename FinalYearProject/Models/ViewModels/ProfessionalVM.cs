using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Models.ViewModels
{
    public class ProfessionalVM
    {

        public ProfessionalVM()
        {

        }


        public ProfessionalVM(Professional row)
        {
            prof_Id = row.prof_Id;
            Prof_FirstName = row.Prof_FirstName;
            Prof_LastName = row.Prof_LastName;
            Prof_Address = row.Prof_Address;
            Prof_EmailID = row.Prof_EmailID;
            Prof_img = row.Prof_img;

        }

        public int prof_Id { get; set; }
        public string Prof_FirstName { get; set; }
        public string Prof_LastName { get; set; }
        public string Prof_EmailID { get; set; }
        public Nullable<System.DateTime> Prof_DateOfBirth { get; set; }
        public string Prof_Password { get; set; }
        public bool Prof_IsEmailVerified { get; set; }
        public Nullable<System.Guid> Prof_ActivationCode { get; set; }
        public string Prof_ResetPasswordCode { get; set; }
        public string Prof_Address { get; set; }
        public string Prof_CNIC { get; set; }
        public string Prof_PhoneNumber { get; set; }
        public bool Prof_IsBanned { get; set; }
        public string Prof_img { get; set; }
        public string Prof_Gender { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }
}