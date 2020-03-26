using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class PatientRecordModel
    {
        [DataMember]
        public Guid RecordId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid AppointmentId { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public String CurrMedication { get; set; }

        [DataMember]
        public String AllergicBool { get; set; }

        [DataMember]
        public string AllergicType { get; set; }

        [DataMember]
        public float BodyTemperature { get; set; }

        [DataMember]
        public String Symptoms { get; set; }

        [DataMember]
        public String Medicines { get; set; }
    }
}
