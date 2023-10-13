using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TicketNow.Domain.Filters
{
    public enum Order
    {
        Ascending = 0,
        Descending = 1
    }

    [DataContract]
    public class _BaseFilter
    {
        public _BaseFilter()
        {
            Skip = 0;
            Take = 10;
            TotalRecords = 0;
            IsPaginated = true;
        }

        [DataMember]
        public int Skip { get; set; }

        [DataMember]
        public int Take { get; set; }

        [DataMember]
        public int TotalRecords { get; set; }

        [DataMember]
        public string SearchText { get; set; }

        [DataMember]
        public bool IsPaginated { get; set; }

        [DataMember]
        public string Sort { get; set; }
    }
}
