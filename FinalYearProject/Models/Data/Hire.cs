using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models.Data
{
    public class Hire
    {

        public int Hire_Id { get; set; }

        public Nullable<System.DateTime> Hire_Date { get; set; }

        public string Hire_Remarks { get; set; }

        public int Cust_Id { get; set; }

        public int prof_Id { get; set; }

        public int Prof_Cat_Id { get; set; }

        public bool IsHired { get; set; }

    }
}