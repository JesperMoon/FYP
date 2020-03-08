using FYP.Entities;
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
        [HttpPost]
        public ActionResult Appointments(string practitionerId)
        {
            PractitionerProcess process = new PractitionerProcess();
            List<AppointmentModel> result = process.GetAppointmentsTable(practitionerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AppointmentAccept(string appointmentId)
        {
            Guid id = Guid.Parse(appointmentId);
            AppointmentModel appointment = new AppointmentModel();
            appointment.AppointmentId = id;
            PractitionerProcess process = new PractitionerProcess();
            int result = process.AppointmentAccept(appointment);

            //if result = 1 then success, if 0 means fail
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AppointmentReject(string appointmentId)
        {
            Guid id = Guid.Parse(appointmentId);
            AppointmentModel appointment = new AppointmentModel();
            appointment.AppointmentId = id;
            PractitionerProcess process = new PractitionerProcess();
            int result = process.AppointmentReject(appointment);

            //if result = 1 then success, if 0 means fail
            return Json(result,JsonRequestBehavior.AllowGet);
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