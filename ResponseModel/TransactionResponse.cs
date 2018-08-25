using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class TransactionResponse
    {
        public int transactionid { get; set; }
        public int payerId { get; set; }
        public int payeeId { get; set; }
        public double paid_amount { get; set; }
        public DateTime paid_on { get; set; }
        public int groupId { get; set; }
    }
}
