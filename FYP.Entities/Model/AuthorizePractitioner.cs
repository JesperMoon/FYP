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
    public class AuthorizePractitionerModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public List<User> PractitionerList = new List<User>();

    }
}
