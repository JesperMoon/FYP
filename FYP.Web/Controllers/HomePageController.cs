using FYP.Entities;
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

            TempData["Deleted"] = "";
            TempData["Pending"] = "";
            TempData["Incorrect"] = "";

            return View(loginInfo);
        }

        [HttpPost]
        public ActionResult PractitionerLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();

            if (Request.Form["Submit"] != null)
            {
                if(ModelState.IsValid == true)
                {
                    PractitionerProcess process = new PractitionerProcess();
                    result = process.PractitionerLogin(loginInfo);

                    TempData["Deleted"] = "";
                    TempData["Pending"] = "";
                    TempData["Incorrect"] = "";

                    //Account found
                    if (result.AccountNo != Guid.Empty)
                    {
                        //Check Status
                        if(result.AccountStatus.Equals(ConstantHelper.AccountStatus.Active))
                        {
                            return RedirectToAction("Home", "Practitioner", result);
                        }
                        else if(result.AccountStatus.Equals(ConstantHelper.AccountStatus.Deleted))
                        {
                            //Account deleted
                            TempData["Deleted"] = "Deleted";
                            loginInfo.Password = "";

                            return View(loginInfo);
                        }
                        else if(result.AccountStatus.Equals(ConstantHelper.AccountStatus.Pending))
                        {
                            //Account pending
                            TempData["Pending"] = "Pending";
                            loginInfo.Password = "";

                            return View(loginInfo);
                        }
                    }
                    else
                    {
                        //Account not found
                        TempData["Incorrect"] = "Incorrect";
                        loginInfo.Password = "";

                        return View(loginInfo);
                    }
                }
            }

            return View(loginInfo);
        }

        public ActionResult PractitionerRegister()
        {
            PractitionerViewModel newUser = new PractitionerViewModel();

            TempData["ConflictEmailAddress"] = "";

            return View(newUser);
        }

        [HttpPost]
        public ActionResult PractitionerRegister(PractitionerViewModel newUser)
        {
            PractitionerProcess process = new PractitionerProcess();

            TempData["ConflictEmailAddress"] = "";

            if (Request.Form["Submit"] != null)
            {
                if (newUser.EmailAddress.Equals(newUser.ReconfirmEmail) && newUser.Password.Equals(newUser.RetypePassword))
                {
                    if (ModelState.IsValid)
                    {
                        int result = process.PractitionerRegister(newUser);

                        if (result == 1)
                        {
                            return RedirectToAction("AccCreateSuccess", "HomePage", null);
                        }
                        else if(result == 2)
                        {
                            TempData["ConflictEmailAddress"] = "ConflictEmailAddress";
                            return View(newUser);
                        }
                        else
                        {
                            return View(newUser);
                        }
                    }
                }
                else
                {
                    return View(newUser);
                }
            }
            return View(newUser);
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