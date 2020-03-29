using FYP.Business;
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
                result = businessLayer.CloseAppointment(patientRecord.AppointmentId);
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
    }
}