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

            RestRequest request = new RestRequest(ConstantHelper.API.Company.CompanyRegister, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newCompany);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        //public int CompanyApproved(CompanyViewModel vm)
        //{
        //    var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

        //    RestRequest request = new RestRequest(ConstantHelper.API.Company.CompanyApproved, Method.POST);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddBody(vm);

        //    IRestResponse<int> response = client.Execute<int>(request);
        //    int result = response.Data;

        //    return result;
        //}

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

        public int ProfileEdit(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.ProfileEdit, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

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

        public List<AppointmentModel> GetAppointmentsTable(string practitionerId)
        {
            PractitionerBaseViewModel vm = new PractitionerBaseViewModel();
            vm.AccId = Guid.Parse(practitionerId);

            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetAppointmentsTable, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<AppointmentModel>> response = client.Execute<List<AppointmentModel>>(request);
            List<AppointmentModel> result = response.Data;

            return result;
        }

        public int AppointmentAccept(AppointmentModel appointment)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Appointment.AppointmentAccepted, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointment);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int AppointmentReject(AppointmentModel appointment)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Appointment.AppointmentRejected, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointment);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int AppointmentAbsent(AppointmentModel appointment)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Appointment.AppointmentAbsent, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointment);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public int AppointmentPending(AppointmentModel appointment)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Appointment.AppointmentPending, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(appointment);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public NewPatientRecordViewModel CreateNewPatientRecord(PatientRecordModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.CreateNewRecord, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<NewPatientRecordViewModel> response = client.Execute<NewPatientRecordViewModel>(request);
            NewPatientRecordViewModel result = response.Data;

            return result;
        }

        public Dictionary<string,string> GetMedicinesDropDown(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.GetMedicinesDropDown, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<PatientRecordModel> response = client.Execute<PatientRecordModel>(request);
            PatientRecordModel result = response.Data;

            return result.MedicineDropDown;
        }

        public int StoreRecordToDB(RecordFileSystem fileSystem,PatientRecordModel patientRecord)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.StoreRecordToDB, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(fileSystem);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;
            int secondResult = 0;

            if(result != 0)
            {
                request = new RestRequest(ConstantHelper.API.Appointment.CloseAppointment, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(patientRecord);

                IRestResponse<int> response2 = client.Execute<int>(request);
                secondResult = response.Data;
            }

            return secondResult;
        }

        public List<PractitionerRecordsDirectory> GetRecordsDirectory(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.GetRecordsDirectory, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PractitionerRecordsDirectory>> response = client.Execute<List<PractitionerRecordsDirectory>>(request);
            List<PractitionerRecordsDirectory> result = response.Data;

            return result;
        }

        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.SearchRecords, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PractitionerRecordsDirectory>> response = client.Execute<List<PractitionerRecordsDirectory>>(request);
            List<PractitionerRecordsDirectory> result = response.Data;

            return result;
        }

        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.PatientRecord.PractitionerGetRecord, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(record);

            IRestResponse<RecordFileSystem> response = client.Execute<RecordFileSystem>(request);
            //byte[] bytes = client.DownloadData(request);
            RecordFileSystem result = new RecordFileSystem();
            //result.FileContents = bytes;
            result = response.Data;

            return result;
        }

        public List<MedicineModel> GetProducts(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetProducts, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<MedicineModel>> response = client.Execute<List<MedicineModel>>(request);
            List<MedicineModel> result = response.Data;

            return result;
        }

        public int CreateProduct(MedicineModel newMedicine)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.CreateProduct, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(newMedicine);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public List<MedicineModel> SearchProduct(MedicineViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.SearchProduct, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<MedicineModel>> response = client.Execute<List<MedicineModel>>(request);
            List<MedicineModel> result = response.Data;

            return result;
        }

        public MedicineModel GetProduct(MedicineModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetProduct, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<MedicineModel> response = client.Execute<MedicineModel>(request);
            MedicineModel result = response.Data;

            return result;
        }

        public int UpdateProduct(MedicineModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.UpdateProduct, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<int> response = client.Execute<int>(request);
            int result = response.Data;

            return result;
        }

        public List<PatientsDirectory> GetPatientsDirectory(PractitionerBaseViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.GetPatientsDirectory, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PatientsDirectory>> response = client.Execute<List<PatientsDirectory>>(request);
            List<PatientsDirectory> result = response.Data;

            return result;
        }

        public List<PatientsDirectory> SearchPatients(PatientsDirectorySearch search)
        {
            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.SearchPatients, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(search);

            IRestResponse<List<PatientsDirectory>> response = client.Execute<List<PatientsDirectory>>(request);
            List<PatientsDirectory> result = response.Data;

            return result;
        }

        public PatientBaseViewModel ViewPatient(PatientBaseViewModel patientModel)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();

            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Patient.PatientProfile, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(patientModel);

            IRestResponse<PatientBaseViewModel> response = client.Execute<PatientBaseViewModel>(request);
            result = response.Data;

            return result;
        }

        public List<PractitionerRecordsDirectory> PatientPractitionerRecords(PractitionerBaseViewModel vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            var client = new RestClient(ConstantHelper.AppSettings.BackEndUrl);

            RestRequest request = new RestRequest(ConstantHelper.API.Practitioner.PatientPractitionerRecords, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<List<PractitionerRecordsDirectory>> response = client.Execute<List<PractitionerRecordsDirectory>>(request);
            result = response.Data;

            return result;
        }
    }
}
