using FYP.Business;
using FYP.Entities;
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
    public class PractitionerController : ApiController
    {
        [Route(ConstantHelper.API.Practitioner.PractitionerLogin)]
        [HttpPost]
        public LoginInfo GetLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.PractitionerLogin(loginInfo);

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetProfile)]
        [HttpPost]
        public PractitionerBaseViewModel GetProfile(PractitionerBaseViewModel vm)
        {
            PractitionerBaseViewModel result = new PractitionerBaseViewModel();

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.GetProfile(vm);

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetAppointmentsTable)]
        [HttpPost]
        public List<AppointmentModel> GetAppointmentsTable(PractitionerBaseViewModel vm)
        {
            List<AppointmentModel> result = new List<AppointmentModel>();

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.GetAppointmentsTable(vm.AccId);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentAccepted)]
        [HttpPost]
        public int AppointmentAccepted(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentAccepted(appointmentModel);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentRejected)]
        [HttpPost]
        public int AppointmentRejected(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentRejected(appointmentModel);

            return result;
        }
    }
}