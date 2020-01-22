using FYP.Data;
using FYP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business
{
    public class PractitionerBusiness
    {
        public PractitionerViewModel PractitionerRegister(PractitionerViewModel vm)
        {
            PractitionerViewModel result = new PractitionerViewModel();

            try
            {
                PractitionerData dataLayer = new PractitionerData();
                result = dataLayer.CreatePractitioner(vm);

            }
            catch(Exception err)
            {

            }

            return result;
        }

        public LoginInfo PractitionerLogin(LoginInfo loginInfo)
        {
            LoginInfo result = new LoginInfo();

            try
            {

            }
            catch(Exception err)
            {

            }

            return result;
        }
    }
}
