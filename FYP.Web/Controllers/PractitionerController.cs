﻿using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using FYP.Process;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static FYP.Framework.EnumConstant;

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
            if(ModelState.IsValid)
            {
                if ((vm.NewPatientRecord.AllergicBool.Equals("No")) || ((vm.NewPatientRecord.AllergicBool.Equals("Yes") && !String.IsNullOrEmpty(vm.NewPatientRecord.AllergicType))))
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
                        //return fileResult;
                        return Content(@"<body>
                           <script type='text/javascript'>
                             if(confirm('Patient Record updated successfully. Press Ok to close current tab.')){ window.close(); window.opener.location.reload(); };
                           </script>
                         </body> ");
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
            return View(vm);
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
                result.PractitionerRecordSearch.Year = DateTime.Today.Year;
                return View(result);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Records(string practitionerId)
        {
            PractitionerBaseViewModel vm = new PractitionerBaseViewModel();
            vm.AccId = Guid.Parse(practitionerId);
            PractitionerProcess process = new PractitionerProcess();
            List<PractitionerRecordsDirectory> result = process.GetRecordsDirectory(vm);
            //pass balck list of records model
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SearchRecords(string recordId, string month, string year, string practitionerId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            if (recordId.Length == 36 || recordId.Length == 4)
            {
                PractitionerRecordSearch vm = new PractitionerRecordSearch();
                if(recordId.Length == 4)
                {
                    vm.RecordId = Guid.Empty;
                }
                else
                {
                    vm.RecordId = Guid.Parse(recordId);
                }
                //vm.Month = (Month)Enum.ToObject(typeof(Month), month);
                vm.Month = (Month)Enum.Parse(typeof(Month), month);
                vm.Year = Convert.ToInt32(year);
                vm.AccId = Guid.Parse(practitionerId);

                if((vm.Year > 1800 && vm.Year < 2200) || String.IsNullOrEmpty(year))
                {
                    PractitionerProcess process = new PractitionerProcess();
                    result = process.SearchRecords(vm);
                }
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult ViewRecord(string recordId, string practitionerId)
        {
            RecordFileSystem record = new RecordFileSystem();
            record.Id = Guid.Parse(recordId);
            record.PractitionerId = Guid.Parse(practitionerId);
            PractitionerProcess process = new PractitionerProcess();
            RecordFileSystem result = new RecordFileSystem();
            result = process.GetRecord(record);

            if (result != null)
            {
                if(result.FileContentsString != null)
                {
                    result.FileContents = Convert.FromBase64String(result.FileContentsString);
                    FileContentResult fileResult = new FileContentResult(result.FileContents, ConstantHelper.AppSettings.RecordFileType);

                    return fileResult;

                }

                return new HttpNotFoundResult("Record Not Found!");
            }
            else
            {
                return new HttpNotFoundResult("Record Not Found!");
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
                vm.Religion = (Religion)Enum.Parse(typeof(Religion), vm.ReligionString);
                vm.Specialist = (Specialist)Enum.Parse(typeof(Specialist), vm.SpecialistString);
                vm.DateOfBirthString = vm.DateOfBirth.ToString("yyyy-MM-dd");

                return View(vm);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfileEdit(PractitionerBaseViewModel vm, Guid accId)
        {
            PractitionerBaseViewModel result = new PractitionerBaseViewModel();
            result.AccId = accId;

            PractitionerProcess process = new PractitionerProcess();
            int returnValue = process.ProfileEdit(vm);

            if (returnValue != 0)
            {
                return RedirectToAction("Profile", "Practitioner", result);
            }
            else
            {
                return Content(@"<body>
                           <script type='text/javascript'>
                             if(confirm('Profile is not updated successfully. Press Ok to try again.')){ window.history.back(); };
                           </script>
                         </body> ");
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