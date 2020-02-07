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
    }
}
