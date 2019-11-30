using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Models.ViewModels
{
    public class ProfessionalCategoryVM
    {
        public ProfessionalCategoryVM()
        {


        }
        public ProfessionalCategoryVM(ProfessionalCategory professionalCategory)
        {

            Prof_Cat_Id = professionalCategory.Prof_Cat_Id;
            Prof_Cat_Name = professionalCategory.Prof_Cat_Name;
            Prof_Cat_Remarks = professionalCategory.Prof_Cat_Remarks;
            Prof_Cat_img = professionalCategory.Prof_Cat_img;

        }
        public int Prof_Cat_Id { get; set; }
        public string Prof_Cat_Name { get; set; }
        public string Prof_Cat_Remarks { get; set; }
        public string Prof_Cat_img { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }


}