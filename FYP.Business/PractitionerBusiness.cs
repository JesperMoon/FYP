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
            PractitionerData data = new PractitionerData();
            result = data.GetAppointmentsTable(practitionerId);

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
                //Change Appointment Status to rejected
                PractitionerData dataLayer = new PractitionerData();
                AppointmentModel model = new AppointmentModel();
                //model is with appointmentdatestring and appointmenttimestring and PatientId + RejectReasons + PracitionerId
                model = dataLayer.AppointmentRejected(appointmentModel);

                if (!model.PatientId.Equals(Guid.Empty))
                {
                    //Get PatientId to retrieve email
                    PatientData patientDataLayer = new PatientData();
                    string patientEmailAddress = patientDataLayer.GetPatientEmail(model.PatientId);
                    PractitionerBaseViewModel companyDetails = new PractitionerBaseViewModel();
                    PractitionerBaseViewModel temp = new PractitionerBaseViewModel();
                    temp.AccId = appointmentModel.PractitionerId;
                    companyDetails = dataLayer.GetProfile(temp);
                    result = SentAppointmentRejectedNotificationEmail(patientEmailAddress, model.AppointmentDateString, model.AppointmentTimeString, model.RejectReasons, companyDetails);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.AppointmentAccepted : " + err);
            }

            return result;
        }

        public int AppointmentAbsent(AppointmentModel appointmentModel)
        {
            int result = 0;

            try
            {
                //Change Appointment Status to absent
                PractitionerData dataLayer = new PractitionerData();
                AppointmentModel model = new AppointmentModel();
                model = dataLayer.AppointmentAbsent(appointmentModel);

                if (!model.PatientId.Equals(Guid.Empty))
                {
                    //Get PatientId to retrieve email
                    PatientData patientDataLayer = new PatientData();
                    string patientEmailAddress = patientDataLayer.GetPatientEmail(model.PatientId);
                    result = SentAppointmentAbsentNotificationEmail(patientEmailAddress, model.AppointmentDateString, model.AppointmentTimeString);
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.AppointmentAbsent : " + err);
            }

            return result;
        }

        public int AppointmentPending(AppointmentModel appointmentModel)
        {
            int result = 0;

            try
            {
                //Change Appointment Status to pending
                PractitionerData dataLayer = new PractitionerData();
                AppointmentModel model = new AppointmentModel();
                model = dataLayer.AppointmentPending(appointmentModel);

                if (!model.PatientId.Equals(Guid.Empty))
                {
                    result = 1;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.AppointmentPending : " + err);
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
                new LogHelper().LogMessage("PractitionerBusiness.SentAppointmentAcceptedNotificationEmail : " + err);
            }

            return result;
        }

        public int SentAppointmentAbsentNotificationEmail(string patientEmailAddress, string appointmentDate, string appointmentTime)
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
                    string patientEmailSubject = ConstantHelper.Email.AppointmentVerification.AppointmentAbsentSubject;
                    string patientEmailBody = ConstantHelper.Email.AppointmentVerification.AppointmentAbsentBody;
                    //replace with appointment details
                    string appointmentDetailsTable = "<table><caption>Appointment Details</caption><tr><th>Appointment Date</th><td>" + appointmentDate + "</td></tr><tr><th>Appointment Time</th><td>" + appointmentTime + "</td></tr></table>";
                    patientEmailBody = patientEmailBody.Replace(ConstantHelper.Email.Keyword.AppointmentDetails, appointmentDetailsTable);
                    EmailHelper.SentMail(mailFrom, patientEmailAddress, patientEmailSubject, patientEmailBody, userName, password);

                    result = 1;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.SentAppointmentAbsentNotificationEmail : " + err);
            }

            return result;
        }

        public int SentAppointmentRejectedNotificationEmail(string patientEmailAddress, string appointmentDate, string appointmentTime, string rejectReasons, PractitionerBaseViewModel companyDetails)
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
                    string patientEmailSubject = ConstantHelper.Email.AppointmentVerification.AppointmentRejectedSubject;
                    string patientEmailBody = ConstantHelper.Email.AppointmentVerification.AppointmentRejectedBody;
                    //replace with appointment details
                    string appointmentDetailsTable = "<table><caption>Appointment Details</caption><tr><th>Appointment Date</th><td>" + appointmentDate + "</td></tr><tr><th>Appointment Time</th><td>" + appointmentTime + "</td></tr></table>";
                    patientEmailBody = patientEmailBody.Replace(ConstantHelper.Email.Keyword.RejectedReasons, rejectReasons);
                    patientEmailBody = patientEmailBody.Replace(ConstantHelper.Email.Keyword.AppointmentDetails, appointmentDetailsTable);
                    EmailHelper.SentMail(mailFrom, patientEmailAddress, patientEmailSubject, patientEmailBody, userName, password);

                    result = 1;
                }
            }
            catch (Exception err)
            {
                new LogHelper().LogMessage("PractitionerBusiness.SentAppointmentRejectedNotificationEmail : " + err);
            }

            return result;
        }

        public NewPatientRecordViewModel CreateNewRecord(PatientRecordModel vm)
        {
            NewPatientRecordViewModel result = new NewPatientRecordViewModel();
            PractitionerData practitionerData = new PractitionerData();
            PatientData patientData = new PatientData();

            //Retrieve practitioner and company information
            PractitionerBaseViewModel practitionerId = new PractitionerBaseViewModel();
            practitionerId.AccId = vm.PractitionerId;
            result.PractitionerDetails = practitionerData.GetProfile(practitionerId);

            // Retrieve patient information
            result.PatientDetails = patientData.PatientProfile(vm.PatientId);

            vm.CreatedOn = DateTime.UtcNow;
            result.NewPatientRecord = vm;

            RecordFileSystem fileRecord = new RecordFileSystem();
            fileRecord.ContentType = ConstantHelper.AppSettings.RecordFileType;
            fileRecord.FileContents = new byte[1];
            fileRecord.FileDownloadname = DateTime.Now.Date.ToString() + "-" + result.PractitionerDetails.CompanyId;
            fileRecord.PatientId = result.PatientDetails.AccId;
            fileRecord.PractitionerId = result.PractitionerDetails.AccId;

            //Creating a record in the database
            result.NewPatientRecord.RecordId = practitionerData.CreatePatientRecord(fileRecord);

            return result;
        }

        public int StoreRecordToDB(RecordFileSystem fileSystem)
        {
            int result = 0;

            PractitionerData data = new PractitionerData();
            result = data.StoreRecordToDB(fileSystem);

            return result;
        }

        public int CloseAppointment(Guid appointmentId)
        {
            int result = 0;

            PractitionerData data = new PractitionerData();
            result = data.CloseAppointment(appointmentId);

            return result;
        }

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(Guid practitionerId)
        {
            PractitionerData dataLayer = new PractitionerData();
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();
            result = dataLayer.GetRecordsDirectory(practitionerId);

            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();
            PractitionerData dataLayer = new PractitionerData();

            if (String.IsNullOrEmpty(vm.Year.ToString()))
            {
                vm.Year = 0;
            }
            result = dataLayer.SearchRecords(vm);

            return result;
        }

        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            PractitionerData dataLayer = new PractitionerData();
            RecordFileSystem result = new RecordFileSystem();
            result = dataLayer.GetRecord(record);

            if(result != null)
            {
                if (result.FileContents != null)
                {
                    result.FileContentsString = Convert.ToBase64String(result.FileContents);
                }
            }

            return result;
        }

        public int ProfileEdit(PractitionerBaseViewModel profile)
        {
            PractitionerData dataLayer = new PractitionerData();
            profile.DateOfBirth = Convert.ToDateTime(profile.DateOfBirthString);

            int result = 0;
            result = dataLayer.ProfileEdit(profile);

            return result;
        }

        public List<MedicineModel> GetProducts(Guid profileId)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            PractitionerData dataLayer = new PractitionerData();
            //MedicineModel row = new MedicineModel();
            //row.ProductCode = "ABC";
            //row.ProductName = "Testing";
            //row.ExpiryDate = DateTime.Now;
            //row.ExpiryDateString = row.ExpiryDate.ToString("dd/MM/yyyy");
            //row.TotalAmount = 15;
            //row.Threshold = 3;
            //result.Add(row);
            result = dataLayer.GetProducts(profileId);

            return result;
        }

        public int CreateProduct(MedicineModel newMedicine)
        {
            int result = 0;

            PractitionerData dataLayer = new PractitionerData();
            result = dataLayer.CreateProduct(newMedicine);

            return result;
        }

        public List<MedicineModel> SearchProduct(MedicineViewModel vm)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            if(String.IsNullOrEmpty(vm.SearchText))
            {
                vm.SearchText = "All";
            }

            if (String.IsNullOrEmpty(vm.ProductCode))
            {
                vm.ProductCode = "All";
            }

            PractitionerData dataLayer = new PractitionerData();
            //MedicineModel row = new MedicineModel();
            //row.ProductCode = "ABC";
            //row.ProductName = "Testing";
            //row.ExpiryDate = DateTime.Now;
            //row.ExpiryDateString = row.ExpiryDate.ToString("dd/MM/yyyy");
            //row.TotalAmount = 15;
            //row.Threshold = 3;
            //result.Add(row);
            result = dataLayer.SearchProduct(vm);

            return result;
        }

        public MedicineModel GetProduct(MedicineModel medicine)
        {
            MedicineModel result = new MedicineModel();

            PractitionerData dataLayer = new PractitionerData();
            result = dataLayer.GetProduct(medicine);

            return result;
        }

        public int UpdateProduct(MedicineModel medicine)
        {
            int result = 0;

            PractitionerData dataLayer = new PractitionerData();
            result = dataLayer.UpdateProduct(medicine);

            return result;
        }


    }
}
