using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;


namespace FYP.Entities.Model
{
    [DataContract]
    [Serializable]
    public class SpecialistNearby
    {
        [DataMember]
        public Guid AccId { get; set; }

        [DataMember]
        public String SpecialistName { get; set; }

        [DataMember]
        public String SpecialistType { get; set; }

        [DataMember]
        public String CompanyName { get; set; }

        [DataMember]
        public String CompanyAddressLine1 { get; set; }

        [DataMember]
        public String CompanyAddressLine2 { get; set; }

        [DataMember]
        public String CompanyAddressLine3 { get; set; }

        [DataMember]
        public String CompanyPhoneNumber { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

        [DataMember]
        public String City { get; set; }

        [DataMember]
        public String State { get; set; }

    }
}
