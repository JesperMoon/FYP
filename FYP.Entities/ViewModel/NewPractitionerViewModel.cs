using FYP.Framework;
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
    public class NewPractitionerViewModel
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
        [Required(ErrorMessage = "Re-confirm email address is required.")]
        [DisplayName("Re-confirm Email Address")]
        [Compare(nameof(EmailAddress), ErrorMessage = "Email address do not match")]
        public string ReconfirmEmail { get; set; }

        [DataMember]
        [DisplayName("Office Phone Number")]
        public string OfficePhoneNumber { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [DataMember]
        public Specialist Specialist { get; set; }

        [DataMember]
        public string Qualification { get; set; }

        [DataMember]
        public byte[] Salt { get; set; }

        [DataMember]
        public int ConflictEmailAddress { get; set; }

        [DataMember]
        public Guid AccId { get; set; }

        //For company drop down
        [DataMember]
        [DisplayName("Company")]
        public Guid CompanyId { get; set; }

        [DataMember]
        public List<string> CompanyIdList { get; set; }

        [DataMember]
        public List<string> CompanyNameList { get; set; }

        [DataMember]
        public Dictionary<string,string> CompanyDropDown { get; set; }

        //For Company view
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string CompanyEmailAddress { get; set; }

        [DataMember]
        public string CompanyAddressLine1 { get; set; }

        [DataMember]
        public string CompanyAddressLine2 { get; set; }

        [DataMember]
        public string CompanyAddressLine3 { get; set; }

        [DataMember]
        public string CompanyPhoneNumber { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

    }
}