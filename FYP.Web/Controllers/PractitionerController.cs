using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public ActionResult AppointmentReject(string appointmentId, string rejectReason)
        {
            Guid id = Guid.Parse(appointmentId);
            AppointmentModel appointment = new AppointmentModel();
            appointment.AppointmentId = id;
            appointment.RejectReasons = rejectReason;
            PractitionerProcess process = new PractitionerProcess();
            int result = process.AppointmentReject(appointment);

            //if result = 1 then success, if 0 means fail
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AppointmentAbsent(string appointmentId)
        {
            Guid id = Guid.Parse(appointmentId);
            AppointmentModel appointment = new AppointmentModel();
            appointment.AppointmentId = id;
            PractitionerProcess process = new PractitionerProcess();
            int result = process.AppointmentAbsent(appointment);

            //if result = 1 then success, if 0 means fail
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        public ActionResult AppointmentPending(string appointmentId)
        {
            Guid id = Guid.Parse(appointmentId);
            AppointmentModel appointment = new AppointmentModel();
            appointment.AppointmentId = id;
            PractitionerProcess process = new PractitionerProcess();
            int result = process.AppointmentPending(appointment);

            //if result = 1 then success, if 0 means fail
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public ActionResult NewPatientRecord(string appointmentId, string practitionerId, string patientId)
        {
            NewPatientRecordViewModel result = new NewPatientRecordViewModel();

            PatientRecordModel newPatientRecord = new PatientRecordModel();
            newPatientRecord.AppointmentId = Guid.Parse(appointmentId);
            newPatientRecord.PractitionerId = Guid.Parse(practitionerId);
            newPatientRecord.PatientId = Guid.Parse(patientId);

            if(!newPatientRecord.AppointmentId.Equals(Guid.Empty) && !newPatientRecord.PractitionerId.Equals(Guid.Empty) && !newPatientRecord.PatientId.Equals(Guid.Empty))
            {
                PractitionerProcess process = new PractitionerProcess();
                result = process.CreateNewPatientRecord(newPatientRecord);
            }
            else
            {
                //no appointment made, retrive practitioner details
            }

            return View(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewPatientRecord(NewPatientRecordViewModel vm)
        {

            if((vm.NewPatientRecord.AllergicBool.Equals("No")) || (vm.NewPatientRecord.AllergicBool.Equals("Yes") && !String.IsNullOrEmpty(vm.NewPatientRecord.AllergicType)))
            {
                //Continue call to API
                var test = vm.NewPatientRecord.CurrMedication;
                var test2 = "Test";

                //return Content(@"<body>
                //       <script type='text/javascript'>
                //         if(confirm('Record created successfully.')){ window.close(); };
                //       </script>
                //     </body> ");

                return View(vm);
            }
            else
            {
                //Stop, return error
                return Json(new { status = "Error", message = "Allergic selected is Yes. The textbox below it cannot be empty." });
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