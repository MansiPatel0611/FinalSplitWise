using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class TransactionGetResponse
    {
       // public int transactionid { get; set; }
        public Detail payerId { get; set; }
        public Detail payeeId { get; set; }
        public double paid_amount { get; set; }
        public DateTime paid_on { get; set; }
        public Detail groupId { get; set; }
    }
}
