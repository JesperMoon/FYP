using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static FYP.Framework.EnumConstant;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class PractitionerRecordSearch
    {
        [DataMember]
        public string RecordId1 { get; set; }

        [DataMember]
        public string RecordId2 { get; set; }

        [DataMember]
        public string RecordId3 { get; set; }

        [DataMember]
        public string RecordId4 { get; set; }

        [DataMember]
        public string RecordId5 { get; set; }

        [DataMember]
        public Guid RecordId { get; set; }

        [DataMember]
        public Month Month { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public Guid AccId { get; set; }
    }
}
