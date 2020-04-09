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
                //vm.HomeAddressLine1 = vm.HomeAddressLine1.TrimEnd(',');
                //vm.HomeAddressLine2 = vm.HomeAddressLine2.TrimEnd(',');
                //vm.HomeAddressLine3 = vm.HomeAddressLine3.TrimEnd(',');

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

            try
            {
                PatientData dataLayer = new PatientData();

                //hashing password
                result = dataLayer.PatientLogin(loginInfo);
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(loginInfo.Password), result.Salt);
                loginInfo.Password = Convert.ToBase64String(hashedPassword);

                if(loginInfo.Password.Equals(result.Password))
                {
                    result.Salt = null;
                    return result;
                }

                new LogHelper().LogMessage("PatientBusiness.PatientLogin : " + "Success");
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PatientBusiness.PatientLogin : " + err);
            }

            return loginInfo;
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

        public List<AuthorizedPractitionersTable> GetAuthorizedPractitionersTable(Guid patientId)
        {
            List<AuthorizedPractitionersTable> result = new List<AuthorizedPractitionersTable>();

            try
            {
                PatientData dataLayer = new PatientData();
                result = dataLayer.GetAuthorizePractitioners(patientId);
            }
            catch(Exception err)
            {
                new LogHelper().LogMessage("PatientBUsiness.GetAuthorizedPractitionersTable : " + err);
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
                new LogHelper().LogMessage("PatientBusiness.MakeAppointment : " + err);
            }
            return result;
        }

        public void SentEmailNotification(AppointmentModel model, PractitionerBaseViewModel ptResult)
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
                    string appointmentDetailsTable = "<table><caption>Appointment Details</caption><tr><th>Appointment Date</th><td>" + tempAppointmentDateTime.ToString("dd/MM/yyyy") + "</td></tr><tr><th>Appointment Time</th><td>" + tempAppointmentDateTime.ToString("hh:mm tt") + "</td></tr><tr><th>Company</th><td>" + ptResult.CompanyName + "</td></tr><tr><th>City</th><td>" + ptResult.City + "</td></tr><tr><th>Postal Code</th><td>" + ptResult.PostalCode + "</td></tr><tr><th>State</th><td>" + ptResult.State + "</td></tr><tr><th>Appointment Description</th><td>" + model.Description + "</td></tr><tr><th>Appointment Remarks</th><td>" + model.Remarks + "</td></tr></table>";
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

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(Guid patientId)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();
            PatientData dataLayer = new PatientData();
            result = dataLayer.GetRecordsDirectory(patientId);
            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch search)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();
            PatientData dataLayer = new PatientData();
            int month = (int)search.Month;
            if(String.IsNullOrEmpty(search.Year.ToString()))
            {
                search.Year = 0;
            }
            result = dataLayer.SearchRecords(search.AccId, search.Year, month);
            return result;
        }

        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            PatientData dataLayer = new PatientData();
            RecordFileSystem result = new RecordFileSystem();
            result = dataLayer.GetRecord(record);

            if (result != null)
            {
                if (result.FileContents != null)
                {
                    result.FileContentsString = Convert.ToBase64String(result.FileContents);
                }
            }

            return result;
        }

        public int ProfileEdit(PatientBaseViewModel profile)
        {
            PatientData dataLayer = new PatientData();
            profile.DateOfBirth = Convert.ToDateTime(profile.DateOfBirthString);

            if (String.IsNullOrEmpty(profile.ContactNumber3))
            {
                profile.ContactNumber3 = "";
            }
            int result = 0;
            result = dataLayer.ProfileEdit(profile);

            return result;
        }

        public List<MedicineModel> ProductSearch(MedicineViewModel search)
        {
            List<MedicineModel> result = new List<MedicineModel>();
            PatientData dataLayer = new PatientData();

            if (!String.IsNullOrEmpty(search.SearchText))
            {
                search.SearchText = "%" + search.SearchText + "%";
            }      
            result = dataLayer.ProductSearch(search);

            return result;
        }

        public CompanyViewModel ViewCompanyProfile(Guid companyId)
        {
            CompanyViewModel result = new CompanyViewModel();

            PatientData data = new PatientData();
            result = data.ViewCompanyProfile(companyId);

            return result;
        }
    }
}
