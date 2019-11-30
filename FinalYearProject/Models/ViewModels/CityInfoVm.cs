using FinalYearProject.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Models.ViewModels
{
    public class CityInfoVm
    {
       public CityInfoVm() {
        }
       public CityInfoVm(CityInfo cityinfo) {
            City_Id = cityinfo.city_Id;
            City_name = cityinfo.city_name;
        }
        public int City_Id { get; set; }
        public string City_name { get; set; }



    }
}