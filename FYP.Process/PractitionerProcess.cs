using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Process
{
    public class PractitionerProcess
    {
        public LoginInfo PractitionerLogin(LoginInfo loginInfo)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.PractitionerLogin, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(loginInfo);

            IRestResponse<LoginInfo> response = client.Execute<LoginInfo>(request);
            LoginInfo result = response.Data;

            return result;
        }

        public int PractitionerRegister(NewPractitionerViewModel newUser)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.PractitionerRegister, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newUser);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int CompanyRegister(CompanyViewModel newCompany)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.CompanyRegister, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newCompany);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public PractitionerBaseViewModel GetProfile(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetProfile, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<PractitionerBaseViewModel> response = client.Execute<PractitionerBaseViewModel>(request);
            PractitionerBaseViewModel result = response.Data;

            return result;
        }

        public NewPractitionerViewModel GetCompanyList(NewPractitionerViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetCompanyList, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<NewPractitionerViewModel> response = client.Execute<NewPractitionerViewModel>(request);
            NewPractitionerViewModel result = response.Data;

            return result;
        }
    }
}
