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
    public class AppointmentViewModel
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public List<AppointmentModel> AppointmentsTable = new List<AppointmentModel>();

        //Store practitioner profile details in Patient site
        [DataMember]
        public PractitionerBaseViewModel Practitioner = new PractitionerBaseViewModel();
    }
}
