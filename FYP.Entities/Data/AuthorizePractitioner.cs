using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities.Data
{
    [DataContract]
    [Serializable]
    public class AuthorizePractitioner
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }
    }
}
