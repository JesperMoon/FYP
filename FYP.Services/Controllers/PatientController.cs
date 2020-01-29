using FYP.Business;
using FYP.Entities;
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
    }
}