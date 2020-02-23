using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Framework
{
    public static partial class ConstantHelper
    {
        public static class AppSettings
        {
            public static string RootSiteUrl = ConfigurationManager.AppSettings["RootSiteUrl"];
            public static string BackEndUrl = ConfigurationManager.AppSettings["BackEndUrl"];

            public static string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"];
            public static string MailFrom = ConfigurationManager.AppSettings["MailFrom"];
            public static string UserName = ConfigurationManager.AppSettings["UserName"];
            public static string Password = ConfigurationManager.AppSettings["Password"];

            public static class Database
            {
                public static string FYP = "FYP";
            }
        }

        public static class DBAppSettings
        {
            public static string FYP = ConfigurationManager.ConnectionStrings["FYP"].ConnectionString;
        }

        public static class StoredProcedure
        {
            public const string GetPractitionerProfile = "[dbo].[GetPractitionerProfile]";
            public const string SpecialistSearch = "[dbo].[SpecialistSearch]";

            public static class Parameter
            {
                public const string AccId = "@AccId";
                public const string SearchText = "@SearchText";
                public const string Specialist = "@Specialist";
                public const string State = "@State";
                public const string PostalCode = "@PostalCode";
            }

        }

        public static class API
        {
            public static class Practitioner
            {
                public const string PractitionerLogin = "HomePage/PractitionerLogin";
                public const string PractitionerRegister = "HomePage/PractitionerRegister";
                public const string PractitionerApproved = "HomePage/PractitionerApproved";
                public const string PractitionerRejected = "HomePage/PractitionerRejected";

                public const string GetCompanyList = "HomePage/GetCompanyList";
                public const string GetProfile = "HomePage/GetProfile";
            }

            public static class Patient
            {
                public const string PatientLogin = "HomePage/PatientLogin";
                public const string PatientRegister = "HomePage/PatientRegister";
                public const string PatientVerification = "HomePage/PatientVerification";
                public const string PatientProfile = "Patient/Profile";

                public const string SpecialistSearch = "Patient/SpecialistSearch";
                public const string AddAuthorizePractitioner = "Patient/AddAuthorizePractitioner";
                public const string MakeAppointment = "Patient/MakeAppointment";
            }

            public static class Company
            {
                public const string CompanyRegister = "HomePage/CompanyRegister";
                public const string CompanyApproved = "HomePage/CompanyApproved";
                public const string CompanyRejected = "HomePage/CompanyRejected";
            }
        }

        public static class AccountStatus
        {
            public const string Pending = "Pending";
            public const string Active = "Active";
            public const string Deleted = "Deleted";
            public const string Rejected = "Rejected";
            public const string NotFound = "Not found";
        }

        public static class Email
        {
            public static class AccountVerification
            {
                public const string EmailSubject = "Web App For Doctor - Account Verification";
                public const string AccountVerifiedSubject = "Web App For Doctor - Account Verificatied";
                public const string AccountRejectedSubject = "Web App For Doctor - Account Rejected";
                public const string PractitionerConfirmEmailSubject = "Web App For Doctor - Practitioner Account Request";

                public const string EmailBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering account on <u>Web App For Doctor</u> .</p> <br /> <a href='{linkToVerify}'>Click here to verify your account</a> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string PractitionerEmailBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering account on <u>Web App For Doctor</u> .</p><p>Your account registering request will be forwarded to the company's email account.</p><p>Do contact your company email address holder to verified the request of this registration.</p> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string PractitionerConfirmEmailBody = @"<html><body><p>Dear User,</p> <br/> <p>There is a new account registering on <u>Web App For Doctor</u> and he/she is waiting your verification on the account.</p> <br /> <p>The details of the practitioner account is as follows. </p> <br /> {practitionerDetailsTable} <br /> <a href='{linkToAccept}'>Approve</a> <br /> <a href='{linkToReject}'>Reject</a> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string PractitionerApprovedBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering your company account on <u>Web App For Doctor</u> .</p> <p>Your account has been activated. You can now access the system and use the system now.</p> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string PractitionerRejectedBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering your company account on <u>Web App For Doctor</u> .</p> <p>Your account request has been rejected.</p><p>Do contact your company's email address holder for further assistance. </p> <br /> <p>Thank you and have a nice day.</p></body></html>";
            }

            public static class CompanyVerification
            {
                public const string EmailSubject = "Web App For Doctor - Company Verification";
                public const string CompanyApprovedSubject = "Web App For Doctor - Company Registration Approved";
                public const string CompanyRejectedSubject = "Web App For Doctor - Company Registration Rejected";

                public const string EmailBody = @"<html><body><p>Dear Admin,</p> <br/> <p>There is a new account registering on <u>Web App For Doctor</u> .</p> <br /> <p>The details of the company is as follows. </p> <br /> {companyDetailsTable} <br /> <a href='{linkToAccept}'>Approve</a> <br /> <a href='{linkToReject}'>Reject</a> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string CompanyEmailBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering account on <u>Web App For Doctor</u> .</p> <p>Please wait admin to verify your company.Your company will be included once the verification is done.</p> <br /><p>Do check your company email inbox regularly for any updates.</p><br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string CompanyApprovedBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering your company account on <u>Web App For Doctor</u> .</p> <p>Your company has been added to our system. Your employees can now select their company now.</p> <br /> <p>Thank you and have a nice day.</p></body></html>";
                public const string CompanyRejectedBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering your company account on <u>Web App For Doctor</u> .</p> <p>Your company was not added to our system. This may cause by your company is not fulfilling our requirements.</p><p>Do contact our admin for further assistance. </p> <br /> <p>Thank you and have a nice day.</p></body></html>";
            }

            public static class AppointmentVerification
            {
                public const string AppointmentMadeSubject = "Web App For Doctor - Appointment Made Successfully";
                public const string NewAppointmentSubject = "Web App For Doctor - New Appointment";
                public const string AppointmentApprovedSubject = "Web App For Doctor - Appointment Approved";
                public const string AppointmentRejectedSubject = "Web App For Doctor - Appointment Rejected";

                public const string AppointmentMadeBody = @"<html><body><p>Dear User,</p><p>The appointment is made successfully. Please check your email regularly for practitioner approval on the appointment.</p><p>Your appointment details is shown as below:</p>{appointmentDetails}<br/><p>Thank you and have a nice day.</p></body></html>";
                public const string NewAppointmentBody = @"<html><body><p>Dear User,</p><p>Sorry for disturbing, this is a reminder emails.</p><p>There is a new appointment from patient. Please login to your account for further actions on the appointment.</p><p>Link to login page : {practitionerLoginPageLink}</p><br/><p>Thank you and have a nice day.</p></body></html>";
                public const string AppointmentApprovedBody = @"<html><body>Web App For Doctor - Appointment Approved</body></html>";
                public const string AppointmentRejectedBody = @"<html><body>Web App For Doctor - Appointment Rejected</body></html>";

            }

            public static class Keyword
            {
                public const string LinkToVerify = "{linkToVerify}";
                
                //Company
                public const string LinkToApprove = "{linkToAccept}";
                public const string LinkToReject = "{linkToReject}";
                public const string companyDetailsTable = "{companyDetailsTable}";

                //Practitioner
                public const string PractitionerDetailsTable = "{practitionerDetailsTable}";

                //Appointment
                public const string AppointmentDetails = "{appointmentDetails}";
                public const string PractitionerLoginPage = "{practitionerLoginPageLink}";
            }
        }

        public static class SQLColumn
        {
            public static class Practitioner
            {
                public const string Id = "Id";
                public const string FirstName = "FirstName";
                public const string LastName = "LastName";
                public const string UserName = "UserName";
                public const string Gender = "Gender";
                public const string Religion = "Religion";
                public const string DateOfBirth = "DateOfBirth";
                public const string EmailAddress = "EmailAddress";
                public const string OfficePhoneNumber = "OfficePhoneNumber";
                public const string Role = "Role";
                public const string Specialist = "Specialist";
                public const string Qualification = "Qualification";

                //Company
                public const string CompanyName = "CompanyName";
                public const string CompanyPhoneNumber = "CompanyPhoneNumber";
                public const string CompanyEmailAddress = "CompanyEmailAddress";
                public const string CompanyAddressLine1 = "CompanyAddressLine1";
                public const string CompanyAddressLine2 = "CompanyAddressLine2";
                public const string CompanyAddressLine3 = "CompanyAddressLine3";
                public const string PostalCode = "PostalCode";
                public const string City = "City";
                public const string State = "State";
            }

            public static class SpecialistSearch
            {
                public const string Id = "Id";
                public const string SpecialistName = "SpecialistName";
                public const string Specialist = "Specialist";
                public const string CompanyName = "CompanyName";
                public const string CompanyAddressLine1 = "CompanyAddressLine1";
                public const string CompanyAddressLine2 = "CompanyAddressLine2";
                public const string CompanyAddressLine3 = "CompanyAddressLine3";
                public const string CompanyPhoneNumber = "CompanyPhoneNumber";
                public const string PostalCode = "PostalCode";
                public const string City = "City";
                public const string State = "State";

            }
        }
    }
}
