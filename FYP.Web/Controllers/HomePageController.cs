using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult PatientLogin()
        {
            LoginInfo loginInfo = new LoginInfo();
            return View(loginInfo);
        }
    }
}