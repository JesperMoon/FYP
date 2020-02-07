using Elmah;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web;

namespace FYP.Framework
{
    public class LogHelper
    {
        public LogHelper()
        {
        }

        public void LogMessage(String message)
        {
            try
            {
                if (System.Web.HttpContext.Current != null)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(message));

                }
                else
                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception(message)));
            }
            catch (Exception ex)
            {
            }
        }

        public void LogMessage(Exception exMessage)
        {
            try
            {
                if (System.Web.HttpContext.Current != null)
                    Elmah.ErrorSignal.FromCurrentContext().Raise(exMessage);
                else
                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(exMessage));
            }
            catch (Exception ex)
            {
            }
        }
    }
}
