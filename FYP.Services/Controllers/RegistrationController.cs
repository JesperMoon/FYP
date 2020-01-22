using FYP.Business;
using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FYP.Services.Controllers
{
    public class RegistrationController : ApiController
    {
        [Route(ConstantHelper.API.Registration.PractitionerRegister)]
        [HttpPost]
        public PractitionerViewModel CreateNewPractitioner(PractitionerViewModel newUser)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            PractitionerViewModel result = businessLayer.PractitionerRegister(newUser);

            return result;
        }
    }
}