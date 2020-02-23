using FYP.Business;
using FYP.Entities;
using FYP.Entities.Model;
using FYP.Entities.ViewModel;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYP.Services.Controllers
{
    public class PatientController : ApiController
    {
        [Route(ConstantHelper.API.Patient.PatientLogin)]
        [HttpPost]
        public LoginInfo GetLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.PatientLogin(loginInfo);

            return result;
        }

        [Route(ConstantHelper.API.Patient.PatientProfile)]
        [HttpPost]
        public PatientBaseViewModel PatientProfile(PatientBaseViewModel vm)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();
            Guid accId = vm.AccId;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.PatientProfile(accId);

            return result;
        }

        [Route(ConstantHelper.API.Patient.SpecialistSearch)]
        [HttpPost]
        public List<SpecialistNearby> SpecialistSearch(SpecialistNearbyViewModel speacialistvm)
        {
            SpecialistNearbyViewModel result = new SpecialistNearbyViewModel();

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.SpecialistSearch(speacialistvm);

            return result.ResultTable;
        }

        [Route(ConstantHelper.API.Patient.AddAuthorizePractitioner)]
        [HttpPost]
        public int AddAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            int result = 0;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.AddAuthorizePractitioner(authorizePractitioner);     //if 1 then added, if 0 means fail

            return result;
        }

        [Route(ConstantHelper.API.Patient.MakeAppointment)]
        [HttpPost]
        public int MakeAppointment(AppointmentModel appointment)
        {
            int result = 0;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.MakeAppointment(appointment);     //if 1 then added, else -1 mean rejected, else 0 is failed

            if(result == 1)
            {
                businessLayer.SentEmailNotification(appointment);
            }

            return result;
        }
    }
}