﻿using FYP.Entities;
using FYP.Entities.Model;
using FYP.Entities.ViewModel;
using FYP.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Process
{
    public class PatientProcess
    {
        public LoginInfo PatientLogin(LoginInfo loginInfo)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.PatientLogin, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(loginInfo);

            IRestResponse<LoginInfo> response = client.Execute<LoginInfo>(request);
            LoginInfo result = response.Data;

            return result;
        }

        public int PatientRegister(PatientViewModel newUser)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.PatientRegister, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newUser);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int PatientVerification(PatientViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.PatientVerification, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public PatientBaseViewModel PatientProfile(PatientBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.PatientProfile, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<PatientBaseViewModel> response = client.Execute<PatientBaseViewModel>(request);
            PatientBaseViewModel result = response.Data;

            return result;
        }

        public List<SpecialistNearby> SpecialistSearch(SpecialistNearbyViewModel speacialistvm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.SpecialistSearch, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(speacialistvm);

            IRestResponse<List<SpecialistNearby>> response = client.Execute<List<SpecialistNearby>>(request);
            List<SpecialistNearby> result = response.Data;

            return result;
        }

        public int AddAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.AddAuthorizePractitioner, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(authorizePractitioner);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int MakeAppointment(AppointmentModel appointmentModel)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.MakeAppointment, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointmentModel);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }
    }
}
