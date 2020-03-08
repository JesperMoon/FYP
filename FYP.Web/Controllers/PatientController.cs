using FYP.Entities;
using FYP.Entities.ViewModel;
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
                return View(vm);
            }

            ////moving data to form vm
            //formVm.FirstName = vm.FirstName;
            //formVm.LastName = vm.LastName;
            //formVm.UserName = vm.UserName;
            //formVm.Gender = vm.Gender;
            ////formVm.Religion = vm.ReligionString;
            //formVm.DateOfBirth = vm.DateOfBirth;
            //formVm.EmailAddress = vm.EmailAddress;
            //formVm.ContactNumber1 = vm.ContactNumber1;
            //formVm.ContactNumber2 = vm.ContactNumber2;
            //formVm.ContactNumber3 = vm.ContactNumber3;
            //formVm.HomeAddressLine1 = vm.HomeAddressLine1;
            //formVm.HomeAddressLine2 = vm.HomeAddressLine2;
            //formVm.HomeAddressLine3 = vm.HomeAddressLine3;
            //formVm.PostalCode = vm.PostalCode;
            //formVm.City = vm.City;
            ////formVm.State = vm.StateString;
            ////formVm.BloodType = vm.BloodTypeString;
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
                return View(result);
            }
        }
    }
}