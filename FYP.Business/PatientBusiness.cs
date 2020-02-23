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
    public class PatientBusiness
    {
        public PatientViewModel PatientRegister(PatientViewModel vm)
        {
            PatientViewModel result = new PatientViewModel();

            try
            {
                if(String.IsNullOrEmpty(vm.ContactNumber3))
                {
                    vm.ContactNumber3 = "";
                }

                var salt = HashingHelper.GenerateSalt();
                vm.Salt = salt;
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(vm.Password), vm.Salt);
                vm.Password = Convert.ToBase64String(hashedPassword);

                //Convert back aligned date time due to restsharp
                vm.DateOfBirth = vm.DateOfBirth.ToLocalTime();

                //Process Home Address
                vm.HomeAddressLine1 = vm.HomeAddressLine1.TrimEnd(',');
                vm.HomeAddressLine2 = vm.HomeAddressLine2.TrimEnd(',');
                vm.HomeAddressLine3 = vm.HomeAddressLine3.TrimEnd(',');

                PatientData dataLayer = new PatientData();
                result = dataLayer.CreatePatient(vm);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientRegister : " + err);
            }

            return result;
        }

        public LoginInfo PatientLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();
            result = loginInfo;

            try
            {
                PatientData dataLayer = new PatientData();

                //hashing password
                var salt = dataLayer.GetSalt(loginInfo.EmailAddress);
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(loginInfo.Password), salt);
                loginInfo.Password = Convert.ToBase64String(hashedPassword);

                result = dataLayer.PatientLogin(loginInfo);
                new LogHelper().LogMessage("PatientBusiness.PatientLogin : " + "Success");
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientLogin : " + err);
            }

            return result;
        }

        public int PatientVerification(Guid accId)
        {
            int result = 0;

            try
            {
                PatientData dataLayer = new PatientData();
                result = dataLayer.PatientVerification(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientVerification : " + err);
            }

            return result;
        }

        public PatientBaseViewModel PatientProfile(Guid accId)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();
            result.AccId = accId;

            try
            {
                PatientData dataLayer = new PatientData();
                result = dataLayer.PatientProfile(accId);
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientProfile : " + err);
            }

            return result;
        }

        public SpecialistNearbyViewModel SpecialistSearch(SpecialistNearbyViewModel specialistvm)
        {
            SpecialistNearbyViewModel result = new SpecialistNearbyViewModel();

            try
            {
                if(String.IsNullOrEmpty(specialistvm.SearchText))
                {
                    specialistvm.SearchText = "All";
                }

                if (String.IsNullOrEmpty(specialistvm.PostalCode))
                {
                    specialistvm.PostalCode = "All";
                }

                PatientData dataLayer = new PatientData();
                result = dataLayer.SpecialistSearch(specialistvm);

                if(result!= null)
                {
                    new LogHelper().LogMessage("PatientBusiness.SpecialistSearch : " + "Success");
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientLogin : " + err);
            }

            return result;
        }

        public int AddAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            AuthorizePractitionerModel check = new AuthorizePractitionerModel();
            int result = 0;

            try
            {
                PatientData dataLayer = new PatientData();
                check = dataLayer.AddAuthorizePractitioner(authorizePractitioner);

                if (!check.PatientId.Equals(Guid.Empty)&& !check.PractitionerId.Equals(Guid.Empty))
                {
                    result = 1;
                    new LogHelper().LogMessage("PatientBusiness.AddAuthorizePractitioner : " + "Success");
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.AddAuthorizePractitioner : " + err);
            }

            return result;
        }

        public int MakeAppointment(AppointmentModel appointmentModel)
        {
            AppointmentModel check = new AppointmentModel();
            int result = 0;

            try
            {
                PatientData dataLayer = new PatientData();
                appointmentModel.AppointmentDate = Convert.ToDateTime(appointmentModel.AppointmentDate).ToLocalTime();
                check = dataLayer.MakeAppointment(appointmentModel);

                if(check.Status == ConstantHelper.AccountStatus.Pending)
                {
                    result = 1;
                }
                else if(check.Status == ConstantHelper.AccountStatus.Rejected)
                {
                    result = -1;
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PatientBUsiness.MakeAppointment : " + err);
            }
            return result;
        }

        public void SentEmailNotification(AppointmentModel model)
        {
            string mailFrom = ConstantHelper.AppSettings.MailFrom;
            string userName = ConstantHelper.AppSettings.UserName;
            string password = ConstantHelper.AppSettings.Password;

            try
            {
                PatientData patientDataLayer = new PatientData();
                string patientEmail = patientDataLayer.GetPatientEmail(model.PatientId);
                if(!String.IsNullOrEmpty(patientEmail))
                {
                    //Sent notification email to patient
                    string patientEmailSubject = ConstantHelper.Email.AppointmentVerification.AppointmentMadeSubject;
                    string patientEmailBody = ConstantHelper.Email.AppointmentVerification.AppointmentMadeBody;
                    //replace with appointment details
                    DateTime tempAppointmentDateTime = Convert.ToDateTime(model.AppointmentDate.ToString());
                    string appointmentDetailsTable = "<table><caption>Appointment Details</caption><tr><th>Appointment Date</th><td>" + tempAppointmentDateTime.ToString("dd/MM/yyyy") + "</td></tr><tr><th>Appointment Time</th><td>" + tempAppointmentDateTime.ToString("hh:mm tt") + "</td></tr><tr><th>Appointment Description</th><td>" + model.Description + "</td></tr><tr><th>Appointment Remarks</th><td>" + model.Remarks + "</td></tr></table>";
                    patientEmailBody = patientEmailBody.Replace(ConstantHelper.Email.Keyword.AppointmentDetails, appointmentDetailsTable);
                    EmailHelper.SentMail(mailFrom, patientEmail, patientEmailSubject, patientEmailBody, userName, password);
                }

                PractitionerData practitionerDataLayer = new PractitionerData();
                string practitionerEmail = practitionerDataLayer.GetPractitionerEmail(model.PractitionerId);
                if (!String.IsNullOrEmpty(practitionerEmail))
                {
                    //Sent notification email to practitioner
                    string practitionerEmailSubject = ConstantHelper.Email.AppointmentVerification.NewAppointmentSubject;
                    string practitionerEmailBody = ConstantHelper.Email.AppointmentVerification.NewAppointmentBody;
                    //replace maybe link to practitionerLogin
                    string practitionerLoginPage = "<a href='" + ConstantHelper.AppSettings.RootSiteUrl + ConstantHelper.API.Practitioner.PractitionerLogin + "'>Practitioner Login Page</a>";
                    practitionerEmailBody = practitionerEmailBody.Replace(ConstantHelper.Email.Keyword.PractitionerLoginPage, practitionerLoginPage);
                    EmailHelper.SentMail(mailFrom, practitionerEmail, practitionerEmailSubject, practitionerEmailBody, userName, password);
                }
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.SentEmailNotification : " + err);
            }
        }
    }
}
