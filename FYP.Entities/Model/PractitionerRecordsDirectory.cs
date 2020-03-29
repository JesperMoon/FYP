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
    public class PractitionerRecordsDirectory
    {
        [DataMember]
        public Guid RecordId { get; set; }

        [DataMember]
        public string CreatedOn { get; set; }

        [DataMember]
        public string CreationTime { get; set; }

        [DataMember]
        public string PatientFirstName { get; set; }

        [DataMember]
        public string PatientLastName { get; set; }
    }
}
