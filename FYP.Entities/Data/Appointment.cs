using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.Data
{
    [DataContract]
    [Serializable]
    public class Appointment : Entity
    {
        [DataMember]
        public DateTime AppointmentDateTime { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string RejectedReasons { get; set; }
    }
}
