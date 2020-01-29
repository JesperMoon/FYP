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
        [Route(ConstantHelper.API.Practitioner.PractitionerRegister)]
        [HttpPost]
        public int CreateNewPractitioner(PractitionerViewModel newUser)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            PractitionerViewModel result = businessLayer.PractitionerRegister(newUser);

            if(result.ConflictEmailAddress == 1)
            {
                return 2;
            }

            if(result != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [Route(ConstantHelper.API.Patient.PatientRegister)]
        [HttpPost]
        public int CreateNewPatient(PatientViewModel newUser)
        {
            PatientBusiness businessLayer = new PatientBusiness();
            PatientViewModel result = businessLayer.PatientRegister(newUser);

            if (result.ConflictEmailAddress == 1)
            {
                return 2;
            }

            if (result != null)
            {
                string mailFrom = ConstantHelper.AppSettings.MailFrom;
                string emailSubject = ConstantHelper.Email.AccountVerification.EmailSubject;
                string emailBody = ConstantHelper.Email.AccountVerification.EmailBody;
                string linkToVerify = ConstantHelper.AppSettings.RootSiteUrl + ConstantHelper.API.Patient.PatientVerification + "?accId=" + result.accId;
                emailBody = emailBody.Replace(ConstantHelper.Email.Keyword.LinkToVerify, linkToVerify);
                string userName = ConstantHelper.AppSettings.UserName;
                string password = ConstantHelper.AppSettings.Password;
                EmailHelper.SentMail(mailFrom, result.EmailAddress, emailSubject, emailBody, userName, password);

                return 1;
            }
            else
            {
                return 0;
            }
        }

        [Route(ConstantHelper.API.Patient.PatientVerification)]
        [HttpPost]
        public int PatientVerification(PatientViewModel vm)
        {
            int result = 0;

            try
            {
                var accId = vm.accId;
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.PatientVerification(accId);
            }
            catch(Exception err)
            {

            }

            return result;
        }
    }
}