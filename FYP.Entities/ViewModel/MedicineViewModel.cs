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
    public class MedicineViewModel
    {
        [DataMember]
        public string SearchText { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }
    }
}
