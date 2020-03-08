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
    public class AuthorizedPractitionersViewModel
    {
        [DataMember]
        public List<AuthorizedPractitionersTable> AuthorizedPractitionersTable = new List<AuthorizedPractitionersTable>();
    }
}
