using FYP.Data;
using FYP.Entities;
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
        public PractitionerViewModel PractitionerRegister(PractitionerViewModel vm)
        {
            PractitionerViewModel result = new PractitionerViewModel();

            try
            {
                if(vm.NewSpecialist != null)
                {
                    vm.Specialist = vm.NewSpecialist;
                }

                var salt = HashingHelper.GenerateSalt();
                vm.Salt = salt;
                var hashedPassword= HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(vm.Password), vm.Salt);
                vm.Password = Convert.ToBase64String(hashedPassword);

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
            result = loginInfo;

            try
            {
                PractitionerData dataLayer = new PractitionerData();

                //hashing password
                var salt = dataLayer.GetSalt(loginInfo.EmailAddress);
                var hashedPassword = HashingHelper.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(loginInfo.Password), salt);
                loginInfo.Password = Convert.ToBase64String(hashedPassword);

                result = dataLayer.PractitionerLogin(loginInfo);
            }
            catch(Exception err)
            {

            }

            return result;
        }

    }
}
