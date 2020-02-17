using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Process;
using FYP.Entities.ViewModel;
//Newly added authentication
using System.Web.Security;

namespace FYP.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //Practitioner
        [AllowAnonymous]
        public ActionResult PractitionerLogin()
        {
            LoginInfo loginInfo = new LoginInfo();

            TempData["Deleted"] = "";
            TempData["Pending"] = "";
            TempData["Incorrect"] = "";

            return View(loginInfo);
        }

        [HttpPost]
        [AllowAnonymous]
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
                            PractitionerBaseViewModel vm = new PractitionerBaseViewModel();
                            vm.AccId = result.AccountNo;
                            FormsAuthentication.SetAuthCookie(result.AccountNo.ToString(), false);
                            return RedirectToAction("Home", "Practitioner", vm);
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

        public ActionResult PractitionerLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("PractitionerLogin", "HomePage", null);
        }

        [AllowAnonymous]
        public ActionResult PractitionerRegister()
        {
            NewPractitionerViewModel newUser = new NewPractitionerViewModel();
            PractitionerProcess process = new PractitionerProcess();
            NewPractitionerViewModel result = process.GetCompanyList(newUser);

            TempData["ConflictEmailAddress"] = "";

            return View(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PractitionerRegister(NewPractitionerViewModel newUser)
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

        //Patient
        [AllowAnonymous]
        public ActionResult PatientLogin()
        {
            LoginInfo loginInfo = new LoginInfo();

            TempData["Deleted"] = "";
            TempData["Pending"] = "";
            TempData["Incorrect"] = "";

            return View(loginInfo);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PatientLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();

            if (Request.Form["Submit"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    PatientProcess process = new PatientProcess();
                    result = process.PatientLogin(loginInfo);

                    TempData["Deleted"] = "";
                    TempData["Pending"] = "";
                    TempData["Incorrect"] = "";

                    //Account found
                    if (result.AccountNo != Guid.Empty)
                    {
                        //Check Status
                        if (result.AccountStatus.Equals(ConstantHelper.AccountStatus.Active))
                        {
                            FormsAuthentication.SetAuthCookie(result.AccountNo.ToString(),false);
                            PatientBaseViewModel vm = new PatientBaseViewModel();
                            vm.AccId = result.AccountNo;
                            return RedirectToAction("Home", "Patient", vm);
                        }

                        else if (result.AccountStatus.Equals(ConstantHelper.AccountStatus.Deleted))
                        {
                            //Account deleted
                            TempData["Deleted"] = "Deleted";
                            loginInfo.Password = "";

                            return View(loginInfo);
                        }

                        else if (result.AccountStatus.Equals(ConstantHelper.AccountStatus.Pending))
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

        public ActionResult PatientLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("PatientLogin","HomePage",null);
        }

        [AllowAnonymous]
        public ActionResult PatientRegister()
        {
            PatientViewModel newUser = new PatientViewModel();

            TempData["ConflictEmailAddress"] = "";

            return View(newUser);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PatientRegister(PatientViewModel newUser)
        {
            PatientProcess process = new PatientProcess();

            TempData["ConflictEmailAddress"] = "";

            if (Request.Form["Submit"] != null)
            {
                if (newUser.EmailAddress.Equals(newUser.ReconfirmEmail) && newUser.Password.Equals(newUser.RetypePassword))
                {
                    if (ModelState.IsValid)
                    {
                        int result = process.PatientRegister(newUser);

                        if (result == 1)
                        {
                            return RedirectToAction("AccCreateSuccess", "HomePage", null);
                        }
                        else if (result == 2)
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

        [AllowAnonymous]
        public ActionResult PatientVerification(Guid accId)
        {
            PatientProcess process = new PatientProcess();
            PatientViewModel vm = new PatientViewModel();
            vm.AccId = accId;
            int result = process.PatientVerification(vm);

            TempData["Verified"] = "Verified";

            if(result == 1)
            {
                TempData["Verified"] = "Verified";
                return View();
            }
            else
            {
                //throw error page
                TempData["Verified"] = "Error";
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult AccCreateSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CompanyApproved()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CompanyRejected()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PractitionerApproved()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PractitionerRejected()
        {
            return View();
        }

        //Company
        [AllowAnonymous]
        public ActionResult CompanyRegister()
        {
            TempData["ConflictEmailAddress"] = "";
            CompanyViewModel newCompany = new CompanyViewModel();
            return View(newCompany);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CompanyRegister(CompanyViewModel newCompany)
        {
            PractitionerProcess process = new PractitionerProcess();

            TempData["ConflictEmailAddress"] = "";

            if (Request.Form["Submit"] != null)
            {
                if (newCompany.CompanyEmailAddress.Equals(newCompany.ReconfirmEmail))
                {
                    if (ModelState.IsValid)
                    {
                        int result = process.CompanyRegister(newCompany);

                        if (result == 1)
                        {
                            TempData["CompanySuccess"] = "CompanySuccess";
                            return RedirectToAction("AccCreateSuccess", "HomePage", null);
                        }
                        else if (result == 2)
                        {
                            TempData["ConflictEmailAddress"] = "ConflictEmailAddress";
                            return View(newCompany);
                        }
                        else
                        {
                            return View(newCompany);
                        }
                    }
                }
                else
                {
                    return View(newCompany);
                }
            }
            return View(newCompany);
        }

    }
}