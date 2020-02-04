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
    }
}