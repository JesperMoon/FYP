﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class RecordFileSystem : Entity
    {
        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public byte[] FileContents { get; set; }

        [DataMember]
        public string FileContentsString { get; set; }

        [DataMember]
        public string FileDownloadname { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public Guid PractitionerId { get; set; }
    }
}
