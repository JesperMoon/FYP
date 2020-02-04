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
    public class PatientViewModel
    {
        [DataMember]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [DataMember]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Religion is required.")]
        public Religion Religion { get; set; }

        [DataMember]
        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string EmailAddress { get; set; }

        [DataMember]
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Invalid password length.")]
        public string Password { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Re-type Password is required.")]
        [DisplayName("Re-type Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Invalid password length.")]
        public string RetypePassword { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Re-confirm password is required.")]
        [DisplayName("Re-confirm Email Address")]
        [Compare(nameof(EmailAddress), ErrorMessage = "Email address do not match")]
        public string ReconfirmEmail { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Contact Number 1 is required.")]
        public string ContactNumber1 { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Contact Number 2 is required.")]
        public string ContactNumber2 { get; set; }

        [DataMember]
        public string ContactNumber3 { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Home Address is required.")]
        public string HomeAddressLine1 { get; set; }

        [DataMember]
        public string HomeAddressLine2 { get; set; }

        [DataMember]
        public string HomeAddressLine3 { get; set; }

        [DataMember]
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Postal Code is required.")]
        [DisplayName("Home Postal Code")]
        [StringLength(5, ErrorMessage = "Invalid postal code.")]
        public string PostalCode { get; set; }

        [DataMember]
        [Required(ErrorMessage = "State is required.")]
        [DisplayName("Home State")]
        public State State { get; set; }

        [DataMember]
        [DisplayName("Blood Type")]
        [Required(ErrorMessage = "Blood Type is required.")]
        public BloodType BloodType { get; set; }

        [DataMember]
        public byte[] Salt { get; set; }

        [DataMember]
        public int ConflictEmailAddress { get; set; }

        [DataMember]
        public Guid AccId { get; set; }
    }
}
