﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserRegister()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }




    }
}