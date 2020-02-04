using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static FYP.Framework.EnumConstant;

namespace FYP.Entities.ViewModel
{
    [DataContract]
    [Serializable]
    public class SpecialistNearbyViewModel
    {
        [DataMember]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special character should not be entered")]
        public string SearchText { get; set; }

        [DataMember]
        public String Specialist { get; set; }

        [DataMember]
        public State EnumState { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

    }
}
