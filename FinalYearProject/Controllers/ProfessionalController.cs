using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using FinalYearProject.Models.ViewModels;

namespace FinalYearProject.Controllers
{
    public class ProfessionalController : Controller
    {

        public ActionResult ProfessionalDashboard() {


            return View();
        }

        public ActionResult ParticularProfessionals(string city)
        {
            var professional = new List<ProfessionalVM>();

            using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
            {

                professional = dc.Professionals.ToArray().Select(x => new ProfessionalVM(x)).ToList();


            }


            return View(professional);
        }

        // GET: Professional
        public ActionResult Pro_Index(Professional professional)
        {
            List<ProfessionalVM> proVMList=new List<ProfessionalVM>();
            using (var context = new EWorkDatabaseEntities())
            {
                try
                {

                    
                    // Init the list
                    proVMList = context.Professionals.ToArray().Select(x => new ProfessionalVM(x)).ToList();


                    //ViewBag.Status = "query.." + query;

             

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Error = "Error.." + ex;
                }
            }
            System.Diagnostics.Debug.WriteLine("" + proVMList);
            return View(proVMList);
        }

        // GET: Professional
        public ActionResult ProfessionalRegistration()
        {
         

                return View();
        }
        [HttpPost]
        public ActionResult ProfessionalRegistration([Bind(Exclude = "IsEmailVerified,ActivationCode")] Professional professional, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/professional_img"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }
                    // ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }


            }


                bool status = false;
             string message = "";
             //Model Validation

             if (ModelState.IsValid)
             {
                 #region
                 //Email is Already exists

                 var isExist = false;// isEmailExist(user.EmailID);
                 if (isExist)
                 {
                     ModelState.AddModelError("EmailExist", "Email is Already exist");
                     return View(professional);

                 }

                 #endregion
                 /*
                 if (file != null && file.ContentLength > 0) {
                     System.Diagnostics.Debug.WriteLine("in the file name");
                     string filename = Path.GetFileName(file.FileName);
                     string imgpath = Path.Combine(Server.MapPath("~/professional_img/"), filename);
                     professional.Prof_img = imgpath;
                     file.SaveAs(imgpath);
                 }
                 */


                 #region// genrate Activation code
                 professional.Prof_ActivationCode = Guid.NewGuid();

                 #endregion

                 #region//Password hashing
                 professional.Prof_Password= Crypto.Hash(professional.Prof_Password);
                 professional.Prof_ConfirmPassword = Crypto.Hash(professional.Prof_ConfirmPassword);
                 professional.Prof_IsEmailVerified = false;
                 #endregion


                 #region
                 //save to database
                 using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
                 {

                     System.Diagnostics.Debug.WriteLine(".........."+"file--"+(file!=null)+"file path---");
                     dc.Professionals.Add(professional);
                     dc.SaveChanges();

                     //send Email to User
                     sendVerificationEmailLink(professional.Prof_EmailID, professional.Prof_ActivationCode.ToString());
                     message = "Registration Sucessfully Done account verification is send to your email ID" + professional.Prof_EmailID;
                     status = true;

                 }
                 #endregion

             }
             else
             {
                 message = "Invalid Request";
             }


             ViewBag.Message = message;
             ViewBag.Status = status;


             return View();
         }

         //verifiy email account
         [HttpGet]
         public ActionResult VerifyAccount(string id)
         {

             bool Status = false;
             using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
             {

                 dc.Configuration.ValidateOnSaveEnabled = false;//this line is added for to avoid the the 
                                                                //confirm password doesnt match issue
                                                                //on save changes

                 var v = dc.Professionals.Where(a => a.Prof_ActivationCode == new Guid(id)).FirstOrDefault();

                 if (v != null)
                 {
                     v.Prof_IsEmailVerified = true;
                     dc.SaveChanges();
                     Status = true;
                 }
                 else
                 {

                     ViewBag.Message = "Invalid Request";
                 }


             }

             ViewBag.Status = Status;
             return View();
         }
        public ActionResult ProfessionalDetail(int id) {

            List<ProfessionalVM> proVM = new List<ProfessionalVM>();

            using (EWorkDatabaseEntities dc=new EWorkDatabaseEntities()) {

                 proVM= dc.Professionals.ToArray().Where(x => x.prof_Id == id).Select(x => new ProfessionalVM(x)).ToList();


            }


            return View(proVM);
        }

         public ActionResult ProfessionalLogin(ProfessionalLogin pslogin, string ReturnUrl)
         {
             System.Diagnostics.Debug.WriteLine("in Login Action");
             string message = "";
            List<ProfessionalVM> proVM = new List<ProfessionalVM>();

            using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
             {

                 var v = dc.Professionals.Where(a => a.Prof_EmailID == pslogin.Prof_EmailID).FirstOrDefault();


               
                System.Diagnostics.Debug.WriteLine("Here is your query message" + v);

                 if (v != null)
                 {
                     System.Diagnostics.Debug.WriteLine("Compare..." + string.Compare(Crypto.Hash(pslogin.Prof_Password), v.Prof_Password));





                     if (string.Compare(Crypto.Hash(pslogin.Prof_Password), v.Prof_Password) == 0)
                     {

                         System.Diagnostics.Debug.WriteLine("Success Login Message..............");
                         int timeout = pslogin.RememberMe ? 525600 : 20;//525600mint=1year
                         var ticket = new FormsAuthenticationTicket(pslogin.Prof_EmailID, pslogin.RememberMe, timeout);
                         string encrypted = FormsAuthentication.Encrypt(ticket);
                         var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                         cookie.Expires = DateTime.Now.AddMinutes(timeout);
                          cookie.HttpOnly = true;
                         Response.Cookies.Add(cookie);

                         if (Url.IsLocalUrl(ReturnUrl))
                         {

                             return Redirect(ReturnUrl);


                         }
                         else
                         {

                             return RedirectToAction("ProfessionalDashboard", "Professional");

                         }

                     }
                     else
                     {

                         message = "Invalid Information";

                     }


                 }
                 else
                 {
                     message = "Invalid Information";


                 }

             }
             
            ViewBag.Message = message;
        
    
            return View();
        }




        [NonAction]
        public bool isEmailExist(string emailID)
        {
            using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
            {

                var v = dc.Professionals.Where(a => a.Prof_EmailID == emailID).FirstOrDefault();
                return v != null;

            }
        }





        [NonAction]
        public void sendVerificationEmailLink(string emailID, string activationCode)
        {

            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("shoaibjamali388@gmail.com", "Email Verification");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "king/king123";
            var subject = "Your Email Successfuly Created";
            var body = "<br></br>Click here for Account verification" +
                "<a href='" + link + "'>" + link + "</a>";





            SmtpClient stmp = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                //Credentials = new NetworkCredential("shoaibjamali388@gmail.com","king/king123")


            };

            using (MailMessage message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true




            })
                stmp.Send(message);
        }


    }
}