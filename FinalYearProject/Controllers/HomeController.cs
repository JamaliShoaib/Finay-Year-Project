using FinalYearProject.Models;
using FinalYearProject.Models.Data;
using FinalYearProject.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using Hire = FinalYearProject.Models.Data;

namespace FinalYearProject.Controllers
{
    public class HomeController : Controller
    {
        

        [HttpPost]
        public ActionResult Test([Bind(Include = "cust_id,pro_id,date")] HireVm hire) {

            /*bool result;
            int noOfRowInserted;
            System.Diagnostics.Debug.WriteLine("Customer Id " +cust_id+"..................Prof_Id "+pro_id);

            System.Diagnostics.Debug.WriteLine("Session  "+Session["UserID"]);
            System.Diagnostics.Debug.WriteLine("Sess  " +date);

            */

            var professional = new List<ProfessionalVM>();

            try {
                using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
                {
                    string str1 = "insert into Hire(Cust_Id,prof_Id,Hire_Date)values(cust_id,prof_id,date)";
                    // string str2 = "Insert Into appointment (id, name, time) Values (id, name, time)";


                    var sql = @"Insert Into Hire (Cust_Id,prof_Id,Hire_Date) Values (@cust_id,@prof_id, @date)";
                   


                    
                    string str2 = "Insert Into Hire.Cust_Id, Hire.prof_Id,Hire.Hire_Date Values cust_id,prof_id,date";
                      var Query = dc.Database.SqlQuery<Models.Hire>(sql).ToList();
                    System.Diagnostics.Debug.WriteLine("Query................."+Query);
                    
                    //noOfRowInserted = dc.Database.ExecuteSqlCommand(str1);

                    //noOfRowInserted = dc.Database.SqlQuery("insert into Hire(Cust_Id,prof_Id,Hire_Date)values(cust_id,prof_id,date)");
                    /*
                    if (noOfRowInserted != 0) {

                        System.Diagnostics.Debug.WriteLine("Record is successfully added......" + noOfRowInserted + " Rows are effected....");

                    }*/
                }
            } catch (Exception e) {

                System.Diagnostics.Debug.WriteLine("Errror is.................."+e);
            }
                return Json("Hello");

            
                
                }

        public ActionResult Index()
        {
            List<ProfessionalCategoryVM> proCatVMs=new List<ProfessionalCategoryVM>();
            using (EWorkDatabaseEntities proCatDatabase=new EWorkDatabaseEntities()) {

                try
                {

                    proCatVMs = proCatDatabase.ProfessionalCategories.ToArray().Select(x => new ProfessionalCategoryVM(x)).ToList();

                    ViewBag.Message = proCatDatabase.ProfessionalCategories.ToArray().Select(x => new ProfessionalCategoryVM(x));

                
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("ERROR!"+e);

                }

            }

            System.Diagnostics.Debug.WriteLine(""+proCatVMs);
                     return View(proCatVMs);
        }
        public ActionResult ProCatDetail(int id) {

            var proCat = new List<ProfessionalCategoryVM>();
            var professional = new List<ProfessionalVM>();
            var city_info = new List<CityInfoVm>();

            using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
            {

                proCat = dc.ProfessionalCategories.ToArray().Where(x => x.Prof_Cat_Id == id).Select(x => new ProfessionalCategoryVM(x)).ToList();
                professional = dc.Professionals.ToArray().Select(x => new ProfessionalVM(x)).ToList();
                city_info = dc.CityInfoes.ToArray().Select(x=>new CityInfoVm(x)).ToList();
                    
                //Where(x => x.Prof_Cat_Id == id).Where(x => x.Prof_City == "Jamshoro").
                
        }

            var viewmodel = new Random_Professional
            {
                ProCat = proCat,
                Professional = professional,
                City_info=city_info

            };

            return View(viewmodel);

            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}