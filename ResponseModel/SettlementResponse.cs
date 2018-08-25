using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class SettlementResponse
    {
        public int settlementid { get; set; }

        public int payerId { get; set; }

        public int payeeId { get; set; }

        public double paid_amount { get; set; }

        public int groupId { get; set; }
    }
    public class GetSettlementResponse
    {
        public Detail payer { get; set; }
        public Detail payee { get; set; }
        public double amount { get; set; }
        public Detail group { get; set; }
    }
}
