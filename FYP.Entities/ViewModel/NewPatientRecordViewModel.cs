using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.ViewModel
{
    [DataContract]
    [Serializable]
    public class NewPatientRecordViewModel
    {
        [DataMember]
        public PatientRecordModel NewPatientRecord { get; set; }

        [DataMember]
        public PatientViewModel PatientDetails { get; set; }

        [DataMember]
        public PractitionerBaseViewModel PractitionerDetails { get; set; }

        public NewPatientRecordViewModel()
        {
            NewPatientRecord = new PatientRecordModel();
            PatientDetails = new PatientViewModel();
            PractitionerDetails =new PractitionerBaseViewModel();
        }
    }
}
