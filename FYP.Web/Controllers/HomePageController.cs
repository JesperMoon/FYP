﻿using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Process;

namespace FYP.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PractitionerLogin()
        {
            LoginInfo loginInfo = new LoginInfo();
            return View(loginInfo);
        }

        [HttpPost]
        public ActionResult PractitionerLogin(LoginInfo loginInfo)
        {
            Guid result;

            if (Request.Form["Submit"] != null)
            {
                if(ModelState.IsValid == true)
                {
                    PractitionerProcess process = new PractitionerProcess();
                    result = process.PractitionerLogin(loginInfo);

                    if (result != Guid.Empty)
                    {
                        return RedirectToAction("Home", "Practitioner", result);
                    }
                    else
                    {
                        loginInfo.Password = "";
                        TempData["Incorrect"] = "incorrect";
                        return View(loginInfo);
                    }
                }
            }

            return View(loginInfo);
        }

        public ActionResult PractitionerRegister()
        {
            PractitionerViewModel newUser = new PractitionerViewModel();
            return View(newUser);
        }

        [HttpPost]
        public ActionResult PractitionerRegister(PractitionerViewModel newUser)
        {
            PractitionerProcess process = new PractitionerProcess();
            if(newUser.EmailAddress.Equals(newUser.ReconfirmEmail) && newUser.Password.Equals(newUser.RetypePassword))
            {
                PractitionerViewModel result = process.PractitionerRegister(newUser);

                if (result != null)
                {
                    return View("Index", "HomePage");
                }

                return View(newUser);
            }
            else
            {
                return View(newUser);
            }
        }

        public ActionResult PatientLogin()
        {
            LoginInfo loginInfo = new LoginInfo();
            return View(loginInfo);
        }

        public ActionResult AccCreateSuccess()
        {
            return View();
        }
    }
}