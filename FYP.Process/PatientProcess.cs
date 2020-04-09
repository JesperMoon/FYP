using FYP.Entities;
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

        public int ProfileEdit(PatientBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.ProfileEdit, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

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

            RestRequest request = new RestRequest(ConstantHelper.API.Appointment.MakeAppointment, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointmentModel);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public List<AuthorizedPractitionersTable> AuthorizedPractitionersTable(PatientBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.AuthorizedPractitionersTable, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<AuthorizedPractitionersTable>> response = client.Execute<List<AuthorizedPractitionersTable>>(request);
            List<AuthorizedPractitionersTable> result = response.Data;

            return result;
        }

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(PatientBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.PatientGetRecordsDirectory, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PractitionerRecordsDirectory>> response = client.Execute<List<PractitionerRecordsDirectory>>(request);
            List<PractitionerRecordsDirectory> result = response.Data;

            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.PatientSearchRecords, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PractitionerRecordsDirectory>> response = client.Execute<List<PractitionerRecordsDirectory>>(request);
            List<PractitionerRecordsDirectory> result = response.Data;

            return result;
        }

        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.PatientGetRecord, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(record);

            IRestResponse<RecordFileSystem> response = client.Execute<RecordFileSystem>(request);
            RecordFileSystem result = new RecordFileSystem();
            result = response.Data;

            return result;
        }

        public List<MedicineModel> ProductSearch(MedicineViewModel search)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.ProductSearch, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(search);

            IRestResponse<List<MedicineModel>> response = client.Execute<List<MedicineModel>>(request);
            List<MedicineModel> result = response.Data;

            return result;
        }

        public CompanyViewModel ViewCompanyProfile(CompanyViewModel input)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.ViewCompanyProfile, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(input);

            IRestResponse<CompanyViewModel> response = client.Execute<CompanyViewModel>(request);
            CompanyViewModel result = response.Data;

            return result;
        }
    }
}
