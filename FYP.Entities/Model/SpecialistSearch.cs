using System;
using System.Runtime.Serialization;

using static FYP.Framework.EnumConstant;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FYP.Entities.Model
{
    [DataContract]
    [Serializable]
    public class SpecialistSearch
    {
        [DataMember]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special character should not be entered")]
        public string SearchText { get; set; }

        [DataMember]
        public Specialist EnumSpecialist  { get; set; }

        [DataMember]
        public string SpecialistSelected { get; set; }

        [DataMember]
        public State EnumState { get; set; }

        [DataMember]
        public string StateSelected { get; set; }

        [DataMember]
        public string PostalCode { get; set; }
    }
}
