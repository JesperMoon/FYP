using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.Data
{
    [DataContract]
    [Serializable]
    public class Practitioner : User
    {
        [Required]
        [DataMember]
        public Guid Company { get; set; }

        [DataMember]
        public string OfficePhoneNumber { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string Specialist { get; set; }

        [DataMember]
        public string Qualification { get; set; }

        //Working hours
    }
}
