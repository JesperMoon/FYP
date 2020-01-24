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
    public class PractitionerController : ApiController
    {
        [Route(ConstantHelper.API.Practitioner.PractitionerLogin)]
        [HttpPost]
        public Guid GetLogin(LoginInfo loginInfo)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            Guid result = businessLayer.PractitionerLogin(loginInfo);

            return result;
        }
    }
}