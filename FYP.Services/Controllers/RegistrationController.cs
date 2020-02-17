using FYP.Business;
using FYP.Entities;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FYP.Services.Controllers
{
    public class RegistrationController : ApiController
    {
        [Route(ConstantHelper.API.Company.CompanyRegister)]
        [HttpPost]
        public int CreateNewCompany(CompanyViewModel newCompany)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            CompanyViewModel result = businessLayer.CompanyRegister(newCompany);

            if (result.ConflictEmailAddress == 1)
            {
                return 2;
            }

            if (result != null)
            {
                try
                {
                    //Sending verification email to admin to verify
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string adminMail = ConstantHelper.AppSettings.AdminEmail;
                    string emailSubject = ConstantHelper.Email.CompanyVerification.EmailSubject;
                    string emailBody = ConstantHelper.Email.CompanyVerification.EmailBody;
                    string linkToApprove = ConstantHelper.AppSettings.BackEndUrl + ConstantHelper.API.Company.CompanyApproved + "?accId=" + result.CompanyId;
                    string linkToReject = ConstantHelper.AppSettings.BackEndUrl + ConstantHelper.API.Company.CompanyRejected + "?accId=" + result.CompanyId;
                    string companyDetailsTable = "<table><tr><th>" + "Company Name" + "</th><td>" + result.CompanyName + "</td></tr><tr><th>" + "Company Phone Number" + "</th><td>" + result.CompanyPhoneNumber + "</td></tr><tr><th>" + "Company Email Address" + "</th><td>" + result.CompanyEmailAddress + "</td></tr><tr><th rowspan='3'>" + "Company Address" + "</th><td>" + result.CompanyAddressLine1 + "</td></tr><tr><td>" + result.CompanyAddressLine2 + "</td></tr><tr><td>" + result.CompanyAddressLine3 + "</td></tr></table>";
                    emailBody = emailBody.Replace(ConstantHelper.Email.Keyword.LinkToApprove, linkToApprove);
                    emailBody = emailBody.Replace(ConstantHelper.Email.Keyword.LinkToReject, linkToReject);
                    emailBody = emailBody.Replace(ConstantHelper.Email.Keyword.companyDetailsTable, companyDetailsTable);
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, adminMail, emailSubject, emailBody, userName, password);

                    //Notify company
                    string emailBodyForCompany = ConstantHelper.Email.CompanyVerification.CompanyEmailBody;
                    EmailHelper.SentMail(mailFrom, result.CompanyEmailAddress, emailSubject, emailBodyForCompany, userName, password);

                }
                catch (Exception err)
                {
                    new LogHelper().LogMessage("RegistrationController.CreateNewCompany : " + err);
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        [Route(ConstantHelper.API.Company.CompanyApproved)]
        [HttpGet]
        public HttpResponseMessage CompanyApproved(Guid accId)
        {
            string companyEmailAddress = String.Empty;

            try
            {
                //var accId = vm.CompanyId;
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                companyEmailAddress = businessLayer.CompanyApproved(accId);

                if(!String.IsNullOrEmpty(companyEmailAddress))
                {
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.CompanyVerification.CompanyApprovedSubject;
                    string emailBody = ConstantHelper.Email.CompanyVerification.CompanyApprovedBody;
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, companyEmailAddress, emailSubject, emailBody, userName, password);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("RegistrationController.CompanyApproved : " + err);
            }

            string route = ConstantHelper.AppSettings.RootSiteUrl + "HomePage/CompanyApproved";

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(route);

            return response;
        }


        [Route(ConstantHelper.API.Company.CompanyRejected)]
        [HttpGet]
        public HttpResponseMessage CompanyRejected(Guid accId)
        {
            string companyEmailAddress = String.Empty;

            try
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                companyEmailAddress = businessLayer.CompanyRejected(accId);

                if (!String.IsNullOrEmpty(companyEmailAddress))
                {
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.CompanyVerification.CompanyRejectedSubject;
                    string emailBody = ConstantHelper.Email.CompanyVerification.CompanyRejectedBody;
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, companyEmailAddress, emailSubject, emailBody, userName, password);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("RegistrationController.CompanyRejected : " + err);
            }

            string route = ConstantHelper.AppSettings.RootSiteUrl + "HomePage/CompanyRejected";

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(route);

            return response;
        }

        [Route(ConstantHelper.API.Practitioner.GetCompanyList)]
        [HttpPost]
        public NewPractitionerViewModel GetCompanyList(NewPractitionerViewModel vm)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            NewPractitionerViewModel result = businessLayer.GetCompanyList(vm);

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.PractitionerRegister)]
        [HttpPost]
        public int CreateNewPractitioner(NewPractitionerViewModel newUser)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            NewPractitionerViewModel result = businessLayer.PractitionerRegister(newUser);

            if(result.ConflictEmailAddress == 1)
            {
                return 2;
            }

            if(result != null)
            {
                try
                {
                    //Sent to Practitioner to inform waiting company actions
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.AccountVerification.EmailSubject;
                    string emailBody = ConstantHelper.Email.AccountVerification.PractitionerEmailBody;
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, result.EmailAddress, emailSubject, emailBody, userName, password);

                    //Sent to Company to request actions
                    string practitionerDetailsTable = "<table><tr><th>" + "Practitioner First Name" + "</th><td>" + result.FirstName + "</td></tr><tr><th>" + "Practitioner Last Name" + "</th><td>" + result.LastName + "</td></tr><tr><th>" + "Practitioner Gender" + "</th><td>" + result.Gender + "</td></tr><tr><th>" + "Practiitoner Email Address" + "</th><td>" + result.EmailAddress + "</td></tr><tr><th>" + "Role" + "</th><td>" + result.Role + "</td></tr><tr><th>" + "Specialist" + "</th><td>" + result.Specialist + "</td></tr></table>";
                    string linkToApprove = ConstantHelper.AppSettings.BackEndUrl + ConstantHelper.API.Practitioner.PractitionerApproved + "?accId=" + result.AccId;
                    string linkToReject = ConstantHelper.AppSettings.BackEndUrl + ConstantHelper.API.Practitioner.PractitionerRejected + "?accId=" + result.AccId;
                    string companyEmailSubject = ConstantHelper.Email.AccountVerification.PractitionerConfirmEmailSubject;
                    string companyEmailBody = ConstantHelper.Email.AccountVerification.PractitionerConfirmEmailBody;
                    companyEmailBody = companyEmailBody.Replace(ConstantHelper.Email.Keyword.PractitionerDetailsTable, practitionerDetailsTable);
                    companyEmailBody = companyEmailBody.Replace(ConstantHelper.Email.Keyword.LinkToApprove,linkToApprove);
                    companyEmailBody = companyEmailBody.Replace(ConstantHelper.Email.Keyword.LinkToReject, linkToReject);
                    EmailHelper.SentMail(mailFrom, result.CompanyEmailAddress, companyEmailSubject, companyEmailBody, userName, password);
                }
                catch (Exception err)
                {
                    new LogHelper().LogMessage("RegistrationController.CreateNewPatient : " + err);
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        [Route(ConstantHelper.API.Practitioner.PractitionerApproved)]
        [HttpGet]
        public HttpResponseMessage PractitionerApproved(Guid accId)
        {
            string practitionerEmailAddress = String.Empty;

            try
            {
                //var accId = vm.CompanyId;
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                practitionerEmailAddress = businessLayer.PractitioenrApproved(accId);

                if (!String.IsNullOrEmpty(practitionerEmailAddress))
                {
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.AccountVerification.AccountVerifiedSubject;
                    string emailBody = ConstantHelper.Email.AccountVerification.PractitionerApprovedBody;
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, practitionerEmailAddress, emailSubject, emailBody, userName, password);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("RegistrationController.PractitionerApproved : " + err);
            }

            string route = ConstantHelper.AppSettings.RootSiteUrl + "HomePage/CompanyApproved";

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(route);

            return response;
        }


        [Route(ConstantHelper.API.Practitioner.PractitionerRejected)]
        [HttpGet]
        public HttpResponseMessage PractitionerRejected(Guid accId)
        {
            string practitionerEmailAddress = String.Empty;

            try
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                practitionerEmailAddress = businessLayer.PractitionerRejected(accId);

                if (!String.IsNullOrEmpty(practitionerEmailAddress))
                {
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.AccountVerification.AccountRejectedSubject;
                    string emailBody = ConstantHelper.Email.AccountVerification.PractitionerRejectedBody;
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, practitionerEmailAddress, emailSubject, emailBody, userName, password);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("RegistrationController.PractitionerRejected : " + err);
            }

            string route = ConstantHelper.AppSettings.RootSiteUrl + "HomePage/CompanyRejected";

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(route);

            return response;
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
                try
                {
                    string mailFrom = ConstantHelper.AppSettings.MailFrom;
                    string emailSubject = ConstantHelper.Email.AccountVerification.EmailSubject;
                    string emailBody = ConstantHelper.Email.AccountVerification.EmailBody;
                    string linkToVerify = ConstantHelper.AppSettings.RootSiteUrl + ConstantHelper.API.Patient.PatientVerification + "?accId=" + result.AccId;
                    emailBody = emailBody.Replace(ConstantHelper.Email.Keyword.LinkToVerify, linkToVerify);
                    string userName = ConstantHelper.AppSettings.UserName;
                    string password = ConstantHelper.AppSettings.Password;
                    EmailHelper.SentMail(mailFrom, result.EmailAddress, emailSubject, emailBody, userName, password);
                }
                catch(Exception err)
                {
                    new LogHelper().LogMessage("RegistrationController.CreateNewPatient : " + err);
                }

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
                var accId = vm.AccId;
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.PatientVerification(accId);
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("RegistrationController.PatientVerification : " + err);
            }

            return result;
        }
    }
}