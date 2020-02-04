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
                PatientBaseViewModel result = new PatientBaseViewModel();
                result.AccId = vm.AccId;
                return View(result);
            }
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