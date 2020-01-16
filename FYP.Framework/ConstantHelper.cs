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
        }

        public static class DBAppSettings
        {
            //public static string DbConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;
        }

        public static class API
        {
            public const string PractitionerLogin = "/HomePage/PractitionerLogin/";
            public const string PatientLogin = "/HomePage/PatientLogin/";
        }

    }
}
