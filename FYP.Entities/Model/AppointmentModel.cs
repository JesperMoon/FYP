using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class AppointmentModel
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid AppointmentId { get; set; }

        [DataMember]
        [DisplayName("Appointment Date")]
        [Required(ErrorMessage ="Appointment Date is required.")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range")]
        public DateTime? AppointmentDate { get; set; }

        [DataMember]
        [DisplayName("Appointment Time")]
        [Required(ErrorMessage = "Appointment Time is required.")]
        public TimeSpan AppointmentTime { get; set; }

        [DataMember]
        public DateTime AppointmentDateTime { get; set; }

        [DataMember]
        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string AppointmentDateString { get; set; }

        [DataMember]
        public string AppointmentTimeString { get; set; }

        [DataMember]
        public string RejectReasons { get; set; }

        [DataMember]
        public string CreatedOnString { get; set; }

        [DataMember]
        public string FirstName { get; set; }
    }
}
