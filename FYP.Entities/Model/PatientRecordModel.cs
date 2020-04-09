using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class PatientRecordModel
    {
        [DataMember]
        public Guid RecordId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }

        [DataMember]
        public Guid AppointmentId { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public String CurrMedication { get; set; }

        [DataMember]
        public String AllergicBool { get; set; }

        [DataMember]
        public string AllergicType { get; set; }

        [DataMember]
        public float BodyTemperature { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Symptoms field cannot be empty.")]
        public String Symptoms { get; set; }

        [DataMember]
        public Dictionary<string, string> MedicineDropDown { get; set; }

        [DataMember]
        public String MedicinesId1 { get; set; }
 
        [DataMember]
        public String MedicinesId2 { get; set; }

        [DataMember]
        public String MedicinesId3 { get; set; }

        [DataMember]
        public String MedicinesId4 { get; set; }

        [DataMember]
        public String MedicinesId5 { get; set; }

        [DataMember]
        public String MedicinesId6 { get; set; }

        [DataMember]
        public String MedicinesId7 { get; set; }

        [DataMember]
        public String MedicinesId8 { get; set; }

        [DataMember]
        public String MedicinesId9 { get; set; }

        [DataMember]
        public String MedicinesId10 { get; set; }

       [DataMember]
        public int QuantityForMedicine1{ get; set; }

        [DataMember]
        public int QuantityForMedicine2 { get; set; }

        [DataMember]
        public int QuantityForMedicine3 { get; set; }

        [DataMember]
        public int QuantityForMedicine4 { get; set; }

        [DataMember]
        public int QuantityForMedicine5 { get; set; }

        [DataMember]
        public int QuantityForMedicine6 { get; set; }

        [DataMember]
        public int QuantityForMedicine7 { get; set; }

        [DataMember]
        public int QuantityForMedicine8 { get; set; }

        [DataMember]
        public int QuantityForMedicine9 { get; set; }

        [DataMember]
        public int QuantityForMedicine10 { get; set; }
    }
}
