using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models.ViewModels
{
    public class HireVm
    {
        HireVm() {

        }
        HireVm(Hire row) {
            Hire_Id = row.Hire_Id;
            Hire_Date = row.Hire_Date;
            Hire_Remarks = row.Hire_Remarks;
            Cust_Id = row.Cust_Id;
            prof_Id = row.prof_Id;
           

        }

        public int Hire_Id { get; set; }

        public Nullable<System.DateTime> Hire_Date { get; set; }

        public string Hire_Remarks { get; set; }

        public int Cust_Id { get; set; }

        public int prof_Id { get; set; }

        public int Prof_Cat_Id { get; set; }

        public bool IsHired { get; set; }

    }
}