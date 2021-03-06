﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.ViewModel
{
    [DataContract]
    [Serializable]
    public class PatientBaseViewModel : PatientViewModel
    {
        [DataMember]
        public string ReligionString { get; set; }

        [DataMember]
        public string StateString { get; set; }

        [DataMember]
        public string BloodTypeString { get; set; }

        [DataMember]
        public SpecialistNearbyViewModel SpecialistNearby = new SpecialistNearbyViewModel();

        [DataMember]
        public MedicineViewModel MedicineViewModel = new MedicineViewModel();

        [DataMember]
        public AuthorizedPractitionersViewModel AuthorizePractitionersViewModel = new AuthorizedPractitionersViewModel();

        [DataMember]
        public PractitionerRecordSearch PractitionerRecordSearch { get; set; }

        public PatientBaseViewModel()
        {
            PractitionerRecordSearch = new PractitionerRecordSearch();
        }
    }
}
