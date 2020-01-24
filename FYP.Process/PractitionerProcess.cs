using FYP.Entities;
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
        public Guid PractitionerLogin(LoginInfo loginInfo)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.PractitionerLogin, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(loginInfo);

            IRestResponse<Guid> response = client.Execute<Guid>(request);
            Guid result = response.Data;

            return result;
        }

        public PractitionerViewModel PractitionerRegister(PractitionerViewModel newUser)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.PractitionerRegister, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newUser);

            IRestResponse<PractitionerViewModel> response = client.Execute<PractitionerViewModel>(request);
            PractitionerViewModel result = response.Data;

            return result;
        }
    }
}
