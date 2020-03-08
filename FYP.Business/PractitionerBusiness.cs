using FYP.Data;
using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business
{
    public class PractitionerBusiness
    {
        public NewPractitionerViewModel PractitionerRegister(NewPractitionerViewModel vm)
        {
            NewPractitionerViewModel result = new NewPractitionerViewModel();

            try
            {
                var salt = HashingHelper.GenerateSalt();
                vm.Salt = salt;
                var hashedPassword= HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(vm.Password), vm.Salt);
                vm.Password = Convert.ToBase64String(hashedPassword);

                //Convert back aligned date time due to restsharp
                vm.DateOfBirth = vm.DateOfBirth.ToLocalTime();

                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.CreatePractitioner(vm);

            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.PractitionerRegister : " + err);
            }

            return result;
        }

        public string PractitioenrApproved(Guid accId)
        {
            string result = String.Empty;

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.PractitionerApproved(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.PractitioenrApproved : " + err);
            }

            return result;
        }

        public string PractitionerRejected(Guid accId)
        {
            string result = String.Empty;

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.PractitionerRejected(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.PractitioenrRejected : " + err);
            }

            return result;
        }

        public PractitionerBaseViewModel GetProfile(PractitionerBaseViewModel vm)
        {
            PractitionerBaseViewModel result = new PractitionerBaseViewModel();

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.GetProfile(vm);

            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.GetProfile : " + err);
            }

            return result;
        }

        public NewPractitionerViewModel GetCompanyList(NewPractitionerViewModel vm)
        {
            NewPractitionerViewModel result = new NewPractitionerViewModel();

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.GetCompanyList(vm);
                //Form key value pair for drop down
                result.CompanyDropDown = result.CompanyIdList.Zip(result.CompanyNameList, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.GetCompanyList : " + err);
            }

            return result;
        }

        public CompanyViewModel CompanyRegister(CompanyViewModel newCompany)
        {
            CompanyViewModel result = new CompanyViewModel();

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.CreateCompany(newCompany);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.CompanyRegister : " + err);
            }

            return result;
        }

        public string CompanyApproved(Guid accId)
        {
            string result = String.Empty;

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.CompanyApproved(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.CompanyApproved : " + err);
            }

            return result;
        }

        public string CompanyRejected(Guid accId)
        {
            string result = String.Empty;

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.CompanyRejected(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.CompanyRejedcted : " + err);
            }

            return result;
        }

        public LoginInfo PractitionerLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();
            result = loginInfo;

            try
            {
                PractitionerData dataLayer = new PractitionerData();

                //hashing password
                result = dataLayer.PractitionerLogin(loginInfo);
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(loginInfo.Password), result.Salt);
                loginInfo.Password = Convert.ToBase64String(hashedPassword);

                if(loginInfo.Password.Equals(result.Password))
                {
                    result.Salt = null;
                    return result;
                }

            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.PractitionerLogin : " + err);
            }

            return result;
        }

        public List<AppointmentModel> GetAppointmentsTable(Guid practitionerId)
        {
            List<AppointmentModel> result = new List<AppointmentModel>();
            PractitionerData business = new PractitionerData();
            result = business.GetAppointmentsTable(practitionerId);

            return result;
        }

        public int AppointmentAccepted(AppointmentModel appointmentModel)
        {
            int result = 0;

            try
            {
                //Change Appointment Status to accepted
                PractitionerData dataLayer = new PractitionerData();
                AppointmentModel model = new AppointmentModel();
                //model is with appointmentdatestring and appointmenttimestring and PatientId
                model = dataLayer.AppointmentAccepted(appointmentModel);

                if(!model.PatientId.Equals(Guid.Empty))
                {
                    //Get PatientId to retrieve email
                    PatientData patientDataLayer = new PatientData();
                    string patientEmailAddress = patientDataLayer.GetPatientEmail(model.PatientId);
                    result = SentAppointmentAcceptedNotificationEmail(patientEmailAddress, model.AppointmentDateString, model.AppointmentTimeString);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.AppointmentAccepted : " + err);
            }

            return result;
        }

        public int AppointmentRejected(AppointmentModel appointmentModel)
        {
            int result = 0;

            try
            {
                //Change Appointment Status to accepted
                PractitionerData dataLayer = new PractitionerData();
                AppointmentModel model = new AppointmentModel();
                //model is with appointmentdatestring and appointmenttimestring and PatientId
                model = dataLayer.AppointmentAccepted(appointmentModel);

                if (!model.PatientId.Equals(Guid.Empty))
                {
                    //Get PatientId to retrieve email
                    PatientData patientDataLayer = new PatientData();
                    string patientEmailAddress = patientDataLayer.GetPatientEmail(model.PatientId);
                    result = SentAppointmentAcceptedNotificationEmail(patientEmailAddress, model.AppointmentDateString, model.AppointmentTimeString);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.AppointmentAccepted : " + err);
            }

            return result;
        }


        public int SentAppointmentAcceptedNotificationEmail(string patientEmailAddress, string appointmentDate, string appointmentTime)
        {
            int result = 0;

            string mailFrom = ConstantHelper.AppSettings.MailFrom;
            string userName = ConstantHelper.AppSettings.UserName;
            string password = ConstantHelper.AppSettings.Password;

            try
            {
                if (!String.IsNullOrEmpty(patientEmailAddress))
                {
                    //Sent notification email to patient
                    string patientEmailSubject = ConstantHelper.Email.AppointmentVerification.AppointmentApprovedSubject;
                    string patientEmailBody = ConstantHelper.Email.AppointmentVerification.AppointmentApprovedBody;
                    //replace with appointment details
                    string appointmentDetailsTable = "<table><caption>Appointment Details</caption><tr><th>Appointment Date</th><td>" + appointmentDate + "</td></tr><tr><th>Appointment Time</th><td>" + appointmentTime + "</td></tr></table>";
                    patientEmailBody = patientEmailBody.Replace(ConstantHelper.Email.Keyword.AppointmentDetails, appointmentDetailsTable);
                    EmailHelper.SentMail(mailFrom, patientEmailAddress, patientEmailSubject, patientEmailBody, userName, password);

                    result = 1;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerData.SentAppointmentAcceptedNotificationEmail : " + err);
            }

            return result;
        }

    }
}
