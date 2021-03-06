﻿using FYP.Business;
using FYP.Entities;
using FYP.Entities.ViewModel;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYP.Services.Controllers
{
    public class PractitionerController : ApiController
    {
        [Route(ConstantHelper.API.Practitioner.PractitionerLogin)]
        [HttpPost]
        public LoginInfo GetLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.PractitionerLogin(loginInfo);

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetProfile)]
        [HttpPost]
        public PractitionerBaseViewModel GetProfile(PractitionerBaseViewModel vm)
        {
            PractitionerBaseViewModel result = new PractitionerBaseViewModel();

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.GetProfile(vm);

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetAppointmentsTable)]
        [HttpPost]
        public List<AppointmentModel> GetAppointmentsTable(PractitionerBaseViewModel vm)
        {
            List<AppointmentModel> result = new List<AppointmentModel>();

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.GetAppointmentsTable(vm.AccId);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentAccepted)]
        [HttpPost]
        public int AppointmentAccepted(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentAccepted(appointmentModel);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentRejected)]
        [HttpPost]
        public int AppointmentRejected(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentRejected(appointmentModel);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentAbsent)]
        [HttpPost]
        public int AppointmentAbsent(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentAbsent(appointmentModel);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.AppointmentPending)]
        [HttpPost]
        public int AppointmentPending(AppointmentModel appointmentModel)
        {
            int result = 0;

            PractitionerBusiness businessLayer = new PractitionerBusiness();
            result = businessLayer.AppointmentPending(appointmentModel);

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.CreateNewRecord)]
        [HttpPost]
        public NewPatientRecordViewModel CreateNewRecord(PatientRecordModel vm)
        {
            NewPatientRecordViewModel result = new NewPatientRecordViewModel();

            if (vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.CreateNewRecord(vm);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.GetMedicinesDropDown)]
        [HttpPost]
        public PatientRecordModel GetMedicinesDropDown(PractitionerBaseViewModel vm)
        {
            PatientRecordModel result = new PatientRecordModel();

            if (vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.GetMedicinesDropDown(vm.CompanyId);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.StoreRecordToDB)]
        [HttpPost]
        public int StoreRecordToDB(RecordFileSystem fileSystem)
        {
            int result = 0;

            if (fileSystem != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.StoreRecordToDB(fileSystem);
            }

            return result;
        }

        [Route(ConstantHelper.API.Appointment.CloseAppointment)]
        [HttpPost]
        public int CloseAppointment(PatientRecordModel patientRecord)
        {
            int result = 0;

            if (patientRecord != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                businessLayer.StockUpdate(patientRecord);

                if(patientRecord.AppointmentId != null || !patientRecord.AppointmentId.Equals(Guid.Empty))
                {
                    result = businessLayer.CloseAppointment(patientRecord.AppointmentId);
                }
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.GetRecordsDirectory)]
        [HttpPost]
        public List<PractitionerRecordsDirectory> GetRecordsDirectory(PractitionerBaseViewModel vm)
        {
            PractitionerBusiness businessLayer = new PractitionerBusiness();
            List<PractitionerRecordsDirectory> result = businessLayer.GetRecordsDirectory(vm.AccId);

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.SearchRecords)]
        [HttpPost]
        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            if(vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.SearchRecords(vm);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.PractitionerGetRecord)]
        [HttpPost]
        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            RecordFileSystem result = new RecordFileSystem();

            if (record != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.GetRecord(record);
                result.FileContents = null;
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.ProfileEdit)]
        [HttpPost]
        public int ProfileEdit(PractitionerBaseViewModel profile)
        {
            int result = 0;

            if (profile != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.ProfileEdit(profile);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetProducts)]
        [HttpPost]
        public List<MedicineModel> GetProducts(PractitionerBaseViewModel profile)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            if (profile != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.GetProducts(profile.AccId);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.CreateProduct)]
        [HttpPost]
        public int CreateProduct(MedicineModel newMedicine)
        {
            int result = 0;

            if (newMedicine != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.CreateProduct(newMedicine);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.SearchProduct)]
        [HttpPost]
        public List<MedicineModel> SearchProduct(MedicineViewModel vm)
        {
            List<MedicineModel> result = new List<MedicineModel>();

            if (vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.SearchProduct(vm);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetProduct)]
        [HttpPost]
        public MedicineModel GetProduct(MedicineModel medicine)
        {
            MedicineModel result = new MedicineModel();

            if (medicine != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.GetProduct(medicine);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.UpdateProduct)]
        [HttpPost]
        public int UpdateProduct(MedicineModel newMedicine)
        {
            int result = 0;

            if (newMedicine != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.UpdateProduct(newMedicine);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.GetPatientsDirectory)]
        [HttpPost]
        public List<PatientsDirectory> GetPatientsDirectory(PractitionerBaseViewModel vm)
        {
            List<PatientsDirectory> result = new List<PatientsDirectory>();

            if (vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.GetPatientsDirectory(vm.AccId);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.SearchPatients)]
        [HttpPost]
        public List<PatientsDirectory> SearchPatients(PatientsDirectorySearch search)
        {
            List<PatientsDirectory> result = new List<PatientsDirectory>();

            if (search != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.SearchPatients(search);
            }

            return result;
        }

        [Route(ConstantHelper.API.Practitioner.PatientPractitionerRecords)]
        [HttpPost]
        public List<PractitionerRecordsDirectory> PatientPractitionerRecords(PractitionerBaseViewModel vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            if (vm != null)
            {
                PractitionerBusiness businessLayer = new PractitionerBusiness();
                result = businessLayer.PatientPractitionerRecords(vm.AccId, vm.PatientBaseViewModel.AccId);
            }

            return result;
        }
    }
}