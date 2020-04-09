using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.ViewModel
{
    [DataContract]
    [Serializable]
    public class PractitionerBaseViewModel : NewPractitionerViewModel
    {
        [DataMember]
        public string ReligionString { get; set; }

        [DataMember]
        public string SpecialistString { get; set; }

        [DataMember]
        public string RejectedReasons { get; set; }

        [DataMember]
        public PractitionerRecordSearch PractitionerRecordSearch { get; set; }

        [DataMember]
        public MedicineViewModel MedicineSearch { get; set; }

        [DataMember]
        public MedicineModel MedicineModel { get; set; }

        [DataMember]
        public PatientsDirectorySearch PatientsDirectorySearch { get; set; }

        [DataMember]
        public PatientBaseViewModel PatientBaseViewModel { get; set; }

        public PractitionerBaseViewModel()
        {
            PractitionerRecordSearch = new PractitionerRecordSearch();
            MedicineSearch = new MedicineViewModel();
            MedicineModel = new MedicineModel();
            PatientsDirectorySearch = new PatientsDirectorySearch();
            PatientBaseViewModel = new PatientBaseViewModel();
        }
        
    }
}
