using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models.ViewModels
{
    public class Random_Professional
    {
        public List<ProfessionalVM> Professional
        { get; set; }
        public List<ProfessionalCategoryVM> ProCat
        { get; set; }
        public IEnumerable<CityInfoVm> City_info
        { get;  set; }
    }
}