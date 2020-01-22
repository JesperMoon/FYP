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
            public static class Registration
            {
                public const string PractitionerRegister = "HomePage/PractitionerRegister";
                public const string PatientRegister = "HomePage/PatientRegister";
            }

            public static class Practitioner
            {
                public const string PractitionerLogin = "HomePage/PractitionerLogin";
                public const string PractitionerRegister = "HomePage/PractitionerRegister";
            }

            public static class Patient
            {
                public const string PatientLogin = "HomePage/PatientLogin";

            }
        }

    }
}
