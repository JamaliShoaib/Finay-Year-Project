using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using FinalYearProject.Models;
using System.Web;

namespace FinalYearProject.Controllers
{
    public class CustomerController : Controller
    {


        [HttpGet]
        public ActionResult CustomerDashboard()
        {
            return View();
        }
        // GET: Customer
        [HttpGet]
        public ActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpGet]
        public Boolean Hire(int cust_id, int pro_id)
        {

            System.Diagnostics.Debug.WriteLine("Ohh yes this fuck is working.......");
            System.Diagnostics.Debug.WriteLine("Customer Id " + cust_id + "..................Prof_Id " + pro_id + "date");

            System.Diagnostics.Debug.WriteLine("Session  " + Session["UserID"]);

      
            return true;
            
        }



        [HttpPost]
            public ActionResult CustomerRegistration([Bind(Exclude = "IsEmailVerified,ActivationCode")]Customer customer )

        {

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
                    return View(customer);

                }

                #endregion

                #region// genrate Activation code
                customer.ActivationCode = Guid.NewGuid();

                #endregion

                #region//Password hashing
                customer.Password = Crypto.Hash(customer.Password);
                customer.ConfirmPassword = Crypto.Hash(customer.ConfirmPassword);
                customer.IsEmailVerified = false;
                #endregion


                #region
                //save to database
                using (EWorkDatabaseEntities dc = new EWorkDatabaseEntities())
                {
                    dc.Customers.Add(customer);
                    dc.SaveChanges();

                    //send Email to User
                    sendVerificationEmailLink(customer.EmailID, customer.ActivationCode.ToString());
                    message = "Registration Sucessfully Done account verification is send to your email ID" + customer.EmailID;
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

                var v = dc.Customers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.IsEmailVerified = true;
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

        //Get:Customer
        [HttpGet]
        public ActionResult CustomerLogin() {


            return View();
        }

        //logout action
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();


            return RedirectToAction("Index", "Home");

        }




        public ActionResult CustomerLogin(CustomerLogin cslogin, string ReturnUrl) { 
         System.Diagnostics.Debug.WriteLine("in Login Action");
            string message = "";

            using (EWorkDatabaseEntities dc=new EWorkDatabaseEntities()) {

                var v = dc.Customers.Where(a => a.EmailID == cslogin.EmailID).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("Here is your query message"+v);

                if (v != null)
                {
                    System.Diagnostics.Debug.WriteLine("Compare..."+ string.Compare(Crypto.Hash(cslogin.Password), v.Password));


                    System.Diagnostics.Debug.WriteLine("URL..." +ReturnUrl);




                    if (string.Compare(Crypto.Hash(cslogin.Password), v.Password) == 0)
                    {


                        


                        System.Diagnostics.Debug.WriteLine("Success Login Message..............");
                int timeout = cslogin.RememberMe ? 525600 : 20;//525600mint=1year
                var ticket = new FormsAuthenticationTicket(cslogin.EmailID, cslogin.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                        Session["UserID"] = v.Cust_Id.ToString();
                        Session["FirstName"] = v.FirstName.ToString();
                        Session["LastName"] = v.LastName.ToString();




                        if (Url.IsLocalUrl(ReturnUrl)) {

                            return Redirect(ReturnUrl);


                }
                        else{

                            return RedirectToAction("Pro_Index", "Professional");

                        }

                    }
                    else {

                        message = "Invalid Information";

                    }


                }
                else {
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

                var v = dc.Customers.Where(a => a.EmailID == emailID).FirstOrDefault();
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