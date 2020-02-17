using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using FYP.Entities.Model;

namespace FYP.Entities.ViewModel
{
    [DataContract]
    [Serializable]
    public class SpecialistNearbyViewModel : SpecialistSearch
    {
        [DataMember]
        public List<SpecialistNearby> ResultTable = new List<SpecialistNearby>();
    }
}
