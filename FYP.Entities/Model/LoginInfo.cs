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
    public class LoginInfo
    {
        [DataMember]
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Password is required.")]
        public string EmailAddress { get; set; }


        [DataMember]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
