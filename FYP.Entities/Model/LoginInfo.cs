using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    public class LoginInfo
    {
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Password is required.")]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
