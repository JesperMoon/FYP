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
    public class AuthorizedPractitionersTable
    {
        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public string PractitionerName { get; set; }

        [DataMember]
        public string Specialist { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string CreatedOn { get; set; }
    }
}
