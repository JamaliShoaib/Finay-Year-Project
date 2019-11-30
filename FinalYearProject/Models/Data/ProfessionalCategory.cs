using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models.Data
{
    [MetadataType(typeof(ProfessionalCategoryMetaData))]

    public partial class ProfessionalCategory
    {
    
    }
    public class ProfessionalCategoryMetaData {

        public int Prof_Cat_Id { get; set; }
        public string Prof_Cat_Name { get; set; }
        public string Prof_Cat_Remarks { get; set; }
        public string Prof_Cat_img { get; set; }

    }
}