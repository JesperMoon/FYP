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
    public class Company : Entity
    {
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string CompanyPhoneNumber { get; set; }

        [DataMember]
        public string CompanyEmailAddress { get; set; }

        [DataMember]
        public string CompanyAddressLine1 { get; set; }

        [DataMember]
        public string CompanyAddressLine2 { get; set; }

        [DataMember]
        public string CompanyAddressLine3 { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }
    }
}
