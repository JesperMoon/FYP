using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.Data
{
    [DataContract]
    [Serializable]
    public class Patient : User
    {
        [DataMember]
        public string ContactNumber1 { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber2 { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber3 { get; set; }

        [DataMember]
        public string HomeAddress { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        [StringLength(10)]
        public string BloodType { get; set; }
    }
}
