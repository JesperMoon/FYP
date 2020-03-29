using FYP.Business;
using FYP.Entities;
using FYP.Entities.Model;
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
    public class PatientController : ApiController
    {
        [Route(ConstantHelper.API.Patient.PatientLogin)]
        [HttpPost]
        public LoginInfo GetLogin(LoginInfo loginInfo)
        {
            LoginInfo result = loginInfo;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.PatientLogin(loginInfo);

            return result;
        }

        [Route(ConstantHelper.API.Patient.PatientProfile)]
        [HttpPost]
        public PatientBaseViewModel PatientProfile(PatientBaseViewModel vm)
        {
            PatientBaseViewModel result = new PatientBaseViewModel();
            Guid accId = vm.AccId;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.PatientProfile(accId);

            return result;
        }

        [Route(ConstantHelper.API.Patient.SpecialistSearch)]
        [HttpPost]
        public List<SpecialistNearby> SpecialistSearch(SpecialistNearbyViewModel speacialistvm)
        {
            SpecialistNearbyViewModel result = new SpecialistNearbyViewModel();

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.SpecialistSearch(speacialistvm);

            return result.ResultTable;
        }

        [Route(ConstantHelper.API.Patient.AddAuthorizePractitioner)]
        [HttpPost]
        public int AddAuthorizePractitioner(AuthorizePractitionerModel authorizePractitioner)
        {
            int result = 0;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.AddAuthorizePractitioner(authorizePractitioner);     //if 1 then added, if 0 means fail

            return result;
        }

        [Route(ConstantHelper.API.Patient.AuthorizedPractitionersTable)]
        [HttpPost]
        public List<AuthorizedPractitionersTable> AuthorizedPractitionersTable(PatientBaseViewModel vm)
        {
            List<AuthorizedPractitionersTable> result = new List<AuthorizedPractitionersTable>();

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.GetAuthorizedPractitionersTable(vm.AccId);

            return result;
        }

        [Route(ConstantHelper.API.Appointment.MakeAppointment)]
        [HttpPost]
        public int MakeAppointment(AppointmentModel appointmentModel)
        {
            int result = 0;

            PatientBusiness businessLayer = new PatientBusiness();
            result = businessLayer.MakeAppointment(appointmentModel);

            if(result == 1)
            {
                PractitionerBaseViewModel pt = new PractitionerBaseViewModel();
                PractitionerBaseViewModel ptResult = new PractitionerBaseViewModel();
                pt.AccId = appointmentModel.PractitionerId;
                PractitionerBusiness practitionerBusiness = new PractitionerBusiness();
                ptResult = practitionerBusiness.GetProfile(pt);
                businessLayer.SentEmailNotification(appointmentModel, ptResult);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.PatientGetRecordsDirectory)]
        [HttpPost]
        public List<PractitionerRecordsDirectory> GetRecordsDirectory(PatientBaseViewModel vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            if (vm != null)
            {
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.GetRecordsDirectory(vm.AccId);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.PatientSearchRecords)]
        [HttpPost]
        public List<PractitionerRecordsDirectory> SearchRecords(PractitionerRecordSearch vm)
        {
            List<PractitionerRecordsDirectory> result = new List<PractitionerRecordsDirectory>();

            if (vm != null)
            {
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.SearchRecords(vm);
            }

            return result;
        }

        [Route(ConstantHelper.API.PatientRecord.PatientGetRecord)]
        [HttpPost]
        public RecordFileSystem GetRecord(RecordFileSystem record)
        {
            RecordFileSystem result = new RecordFileSystem();

            if (record != null)
            {
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.GetRecord(record);
                result.FileContents = null;
            }

            return result;
        }

        [Route(ConstantHelper.API.Patient.ProfileEdit)]
        [HttpPost]
        public int ProfileEdit(PatientBaseViewModel profile)
        {
            int result = 0;

            if (profile != null)
            {
                PatientBusiness businessLayer = new PatientBusiness();
                result = businessLayer.ProfileEdit(profile);
            }

            return result;
        }
    }
}