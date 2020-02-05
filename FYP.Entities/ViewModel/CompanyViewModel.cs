using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static FYP.Framework.EnumConstant;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class CompanyViewModel
    {
        [DataMember]
        [Required(ErrorMessage = "Company Name cannot be empty.")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Company Phone Number cannot be empty.")]
        [DisplayName("Company Phone Number")]
        public string CompanyPhoneNumber { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Company Email Address cannot be empty.")]
        [DisplayName("Company Email Address")]
        public string CompanyEmailAddress { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Re-confirm email address is required.")]
        [DisplayName("Re-confirm Email Address")]
        [Compare(nameof(CompanyEmailAddress), ErrorMessage = "Email address do not match")]
        public string ReconfirmEmail { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Company Address cannot be empty.")]
        [DisplayName("Company Address")]
        public string CompanyAddressLine1 { get; set; }

        [DataMember]
        public string CompanyAddressLine2 { get; set; }

        [DataMember]
        public string CompanyAddressLine3 { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(5, ErrorMessage = "Invalid postal code.")]
        [DisplayName("Company Postal Code")]
        public string PostalCode { get; set; }

        [DataMember]
        [DisplayName("Company City")]
        public string City { get; set; }

        [DataMember]
        [DisplayName("Company State")]
        public State State { get; set; }

        [DataMember]
        public int ConflictEmailAddress { get; set; }
    }
}
