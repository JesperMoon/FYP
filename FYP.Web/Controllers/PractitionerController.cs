﻿using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using FYP.Process;
using System;
using System.Collections.Generic;
using System.IO;
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

            //for appointment users
            if(!newPatientRecord.AppointmentId.Equals(Guid.Empty) && !newPatientRecord.PractitionerId.Equals(Guid.Empty) && !newPatientRecord.PatientId.Equals(Guid.Empty))
            {
                PractitionerProcess process = new PractitionerProcess();
                result = process.CreateNewPatientRecord(newPatientRecord);
            }
            //for not appointment user
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

            if((vm.NewPatientRecord.AllergicBool.Equals("No")) || ((vm.NewPatientRecord.AllergicBool.Equals("Yes") && !String.IsNullOrEmpty(vm.NewPatientRecord.AllergicType))))
            {
                //Conversion to pdf
                var viewToString = RenderViewToString(ControllerContext, "~/Views/Shared/RecordTemplate.cshtml", vm, false);

                FileContentResult fileResult = new FileContentResult(PdfSharpConvert(viewToString), ConstantHelper.AppSettings.RecordFileType);

                RecordFileSystem fileRecord = new RecordFileSystem();
                fileRecord.Id = vm.NewPatientRecord.RecordId;
                fileRecord.FileContents = fileResult.FileContents;
                fileRecord.FileDownloadname = DateTime.Now.ToString("dd-MM-yyyy") + "-" + vm.PractitionerDetails.CompanyId + ".pdf";

                //Continue call to API
                PractitionerProcess process = new PractitionerProcess();
                int returnResult = process.StoreRecordToDB(fileRecord, vm.NewPatientRecord);

                if (returnResult != 0)
                {
                    return Content(@"<body>
                           <script type='text/javascript'>
                             if(confirm('Patient Record updated successfully. Press Ok to close current tab.')){ window.close(); window.opener.location.reload(); };
                           </script>
                         </body> "); ;
                }
                else
                {
                    return Content(@"<body>
                           <script type='text/javascript'>
                             if(confirm('Fail to insert record to database. Press ok to go back.')){ window.history.back(); };
                           </script>
                         </body> ");
                }
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


        //For pdf conversion
        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
            {
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            }
            else
            {
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
            }

            if (viewEngineResult == null)
            {
                throw new FileNotFoundException("View cannot be found.");
            }

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }


            return result.Trim();
        }
    }
}