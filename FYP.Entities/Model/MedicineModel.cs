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
    public class MedicineModel
    {
        [DataMember]
        public Guid MedicineId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        [DisplayName("Product Code")]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [DisplayName("Product Name")]
        [Required]
        public string ProductName { get; set; }

        [DataMember]
        [DisplayName("Production Date")]
        public DateTime ProductionDate { get; set; }

        [DataMember]
        public string ProductionDateString { get; set; }

        [DataMember]
        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [DataMember]
        public string ExpiryDateString { get; set; }

        [DataMember]
        [DisplayName("Total Amount")]
        public int TotalAmount { get; set; }

        [DataMember]
        [DisplayName("Threshold")]
        [DefaultValue(30)]
        public int Threshold { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }
    }
}
