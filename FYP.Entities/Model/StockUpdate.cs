using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Entities
{
    [DataContract]
    [Serializable]
    public class StockUpdate
    {
        [DataMember]
        public Guid MedicineId { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
