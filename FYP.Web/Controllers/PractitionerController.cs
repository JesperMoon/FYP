using FYP.Entities.ViewModel;
using FYP.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class PractitionerController : Controller
    {
        // GET: Practitioner
        [Authorize]
        public ActionResult Home(PractitionerBaseViewModel vm)
        {
            if(vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        public ActionResult Appointments(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        public ActionResult Products(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        public ActionResult Records(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        public ActionResult Patients(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        public ActionResult Profile(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                PractitionerBaseViewModel result = new PractitionerBaseViewModel();
                PractitionerProcess process = new PractitionerProcess();
                result = process.GetProfile(vm);
                return View(result);
            }
        }

        [Authorize]
        public ActionResult ProfileEdit(PractitionerBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PractitionerLogin", "HomePage", null);
            }
            else
            {
                return View(vm);
            }
        }

    }
}