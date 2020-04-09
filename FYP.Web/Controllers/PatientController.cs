using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using FYP.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FYP.Framework.EnumConstant;

namespace FYP.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        //Must Pass Account Id with BaseViewModel
        [Authorize]
        public ActionResult Home(PatientBaseViewModel vm)
        {
            if(vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {
                PatientProcess process = new PatientProcess();
                PatientBaseViewModel result = process.PatientProfile(vm);

                result.DateOfBirth = result.DateOfBirth.ToLocalTime();

                if (result.AccId.Equals(Guid.Empty))
                {
                    return RedirectToAction("PatientLogin", "HomePage", null);
                }
                else if (result != null)
                {
                    return View(result);
                }
                else
                {
                    return RedirectToAction("PatientLogin", "HomePage", null);
                }
            }
        }

        [Authorize]
        public ActionResult ProfileEdit(PatientBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {
                vm.State = (State)Enum.Parse(typeof(State), vm.StateString);
                vm.Religion = (Religion)Enum.Parse(typeof(Religion), vm.ReligionString);
                vm.DateOfBirthString = vm.DateOfBirth.ToString("yyyy-MM-dd");

                return View(vm);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfileEdit(PatientBaseViewModel vm, Guid accId)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();
            result.AccId = accId;

            PatientProcess process = new PatientProcess();
            int returnValue = process.ProfileEdit(vm);


            if (returnValue != 0)
            {
                return RedirectToAction("Home", "Patient", result);
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

        [Authorize]
        public ActionResult SpecialistNearby(PatientBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {

                //vm.SpecialistNearby.Specialist = Enum.GetValues(typeof(Specialist)).Cast<Specialist>().Select(v => v.ToString()).ToList();
                //vm.SpecialistNearby.State = Enum.GetValues(typeof(State)).Cast<State>().Select(v => v.ToString()).ToList();
                return View(vm);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult SpecialistNearby(string searchText, int specialist, int state, string postalCode)
        {
            var selectedSpecialist = (Specialist)specialist;
            var selectedState = (State)state;

            SpecialistNearbyViewModel specialistvm = new SpecialistNearbyViewModel();
            specialistvm.SearchText = searchText;                                           //Search Text
            specialistvm.SpecialistSelected = selectedSpecialist.ToString();                //Specialist       
            specialistvm.StateSelected = selectedState.ToString();                          //State
            specialistvm.PostalCode = postalCode;                                           //Posstal Code


            SpecialistNearbyViewModel result = new SpecialistNearbyViewModel();
            PatientProcess process = new PatientProcess();
            result.ResultTable = process.SpecialistSearch(specialistvm);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SpecialistProfile(string specialistId, string accId)
        {
            AppointmentViewModel result = new AppointmentViewModel();
            PractitionerBaseViewModel practitioner = new PractitionerBaseViewModel();

            if (String.IsNullOrEmpty(specialistId) && String.IsNullOrEmpty(accId))
            {
                return new HttpNotFoundResult( "Oops..There is no account selected. Please try again.");
            }
            else
            {
                Guid patientAccId = Guid.Parse(accId);
                result.PatientId = patientAccId;

                Guid specialistAccId = Guid.Parse(specialistId);
                if (specialistAccId.Equals(Guid.Empty))
                {
                    return new HttpNotFoundResult("Oops..There is some errors occur. Please try again.");
                }
                else
                {
                    PractitionerBaseViewModel vm = new PractitionerBaseViewModel();
                    vm.AccId = specialistAccId;
                    PractitionerProcess process = new PractitionerProcess();
                    practitioner = process.GetProfile(vm);

                    result.Practitioner = practitioner;
                }
            }

            return View(result);
        }

        [Authorize]
        public ActionResult MakeAppointment(string patientId, string practitionerId)
        {
            int result = 0;

            PatientProcess process = new PatientProcess();
            AuthorizePractitionerModel authorizedPractitioner = new AuthorizePractitionerModel();
            if(patientId != null && practitionerId != null)
            {
                authorizedPractitioner.PatientId = Guid.Parse(patientId);
                authorizedPractitioner.PractitionerId = Guid.Parse(practitionerId);

                result = process.AddAuthorizePractitioner(authorizedPractitioner);
            }

            if(result == 1)
            {
                //Create new Appointment View Model
                AppointmentModel model = new AppointmentModel();
                TempData["Rejected"] = "";
                TempData["DateRange"] = "";

                return View(model);
            }
            else
            {
                return new HttpNotFoundResult("Failed to add practitioner to your account's authorized practitioner list.");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeAppointment(AppointmentModel appointmentModel)
        {
            TempData["Rejected"] = "";
            TempData["DateRange"] = "";

            if (ModelState.IsValid)
            {
                if (appointmentModel.AppointmentDate >= DateTime.Now)
                {
                    appointmentModel.AppointmentDate = appointmentModel.AppointmentDate + appointmentModel.AppointmentTime;
                    PatientProcess process = new PatientProcess();
                    int result = process.MakeAppointment(appointmentModel);
                    if(result == 1)         //success
                    {
                        return Content(@"<body>
                       <script type='text/javascript'>
                         if(confirm('Appointment is made successfully. Please wait and check your email for practitioner approval. Press ok to close this tab.')){ window.close(); };
                       </script>
                     </body> ");
                    }
                    else if(result == -1)   //rejected
                    {
                        TempData["Rejected"] = "Rejected";
                        return View();
                    }
                    else                    //failed
                    {
                        return View();
                    }
                }
                else
                {
                    TempData["DateRange"] = "DateRange";
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult Medicine(PatientBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {
                PatientBaseViewModel result = new PatientBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProductSearch(string searchText)
        {
            MedicineViewModel search = new MedicineViewModel();
            search.SearchText = searchText;
            List<MedicineModel> result = new List<MedicineModel>();
            PatientProcess process = new PatientProcess();
            result = process.ProductSearch(search);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult ViewCompanyProfile(string companyId)
        {
            if(companyId != null)
            {
                CompanyViewModel result = new CompanyViewModel();
                CompanyViewModel input = new CompanyViewModel();
                input.CompanyId = Guid.Parse(companyId);

                PatientProcess process = new PatientProcess();

                result = process.ViewCompanyProfile(input);
                return View(result);
            }
            else
            {
                return new HttpNotFoundResult("Company Not Found! Please try again.");
            }

        }

        [Authorize]
        public ActionResult AuthorizedUsers(PatientBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {
                PatientBaseViewModel result = new PatientBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AuthorizedUsers(string patientId)
        {
            AuthorizedPractitionersViewModel result = new AuthorizedPractitionersViewModel();
            PatientBaseViewModel vm = new PatientBaseViewModel();
            vm.AccId = Guid.Parse(patientId);

            PatientProcess process = new PatientProcess();
            result.AuthorizedPractitionersTable = process.AuthorizedPractitionersTable(vm);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Records(PatientBaseViewModel vm)
        {
            if (vm.AccId.Equals(Guid.Empty))
            {
                return RedirectToAction("PatientLogin", "HomePage", null);
            }
            else
            {
                PatientBaseViewModel result = new PatientBaseViewModel();
                result.AccId = vm.AccId;
                result.PractitionerRecordSearch.Year = DateTime.Now.Year;
                return View(result);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Records(string patientId)
        {
            PatientBaseViewModel vm = new PatientBaseViewModel();
            vm.AccId = Guid.Parse(patientId);
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();
            PatientProcess process = new PatientProcess();
            result = process.GetRecordsDirectory(vm);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SearchRecords(string month, string year, string patientId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            PractitionerRecordSearch vm = new PractitionerRecordSearch();
            vm.AccId = Guid.Parse(patientId);
            vm.Month = (Month)Enum.Parse(typeof(Month), month);
            vm.Year = Convert.ToInt32(year);

            PatientProcess process = new PatientProcess();
            result = process.SearchRecords(vm);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult ViewRecord(string recordId, string patientId)
        {
            RecordFileSystem record = new RecordFileSystem();
            record.Id = Guid.Parse(recordId);
            record.PatientId = Guid.Parse(patientId);
            PatientProcess process = new PatientProcess();
            RecordFileSystem result = new RecordFileSystem();
            result = process.GetRecord(record);

            if (result != null)
            {
                if (result.FileContentsString != null)
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
    }
}