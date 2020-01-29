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

        public static class API
        {
            public static class Practitioner
            {
                public const string PractitionerLogin = "HomePage/PractitionerLogin";
                public const string PractitionerRegister = "HomePage/PractitionerRegister";
            }

            public static class Patient
            {
                public const string PatientLogin = "HomePage/PatientLogin";
                public const string PatientRegister = "HomePage/PatientRegister";
                public const string PatientVerification = "HomePage/PatientVerification";
            }
        }

        public static class AccountStatus
        {
            public const string Pending = "Pending";
            public const string Active = "Active";
            public const string Deleted = "Deleted";
            public const string NotFound = "Not found";
        }

        public static class Email
        {
            public static class AccountVerification
            {
                public const string EmailSubject = "Web App For Doctor - Account Verification";
                public const string EmailBody = @"<html><body><p>Dear User,</p> <br/> <p>Thank you for registering account on <u>Web App For Doctor</u> .</p> <br /> <a href='{linkToVerify}'>Click here to verify your account</a> <br /> <p>Thank you and have a nice day.</p></body></html>";
            }

            public static class Keyword
            {
                public const string LinkToVerify = "{linkToVerify}";
            }
        }
    }
}
