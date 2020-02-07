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
                if(vm.NewSpecialist != null)
                {
                    vm.Specialist = vm.NewSpecialist;
                }

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
                new LogHelper().LogMessage("PractitionerBusiness.PractitionerLogin : " + err);
            }

            return result;
        }

    }
}
