using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class BillResponse
    {
        public int billid { get; set; }
        public string description { get; set; }
        public double total_amount { get; set; }
        public int bill_created_byId { get; set; }
        public int bill_updated_byId { get; set; }
        public int groupId { get; set; }
        public DateTime bill_date { get; set; }
        public DateTime bill_created_at { get; set; }
        public DateTime bill_updated_at { get; set; }
        public List<PayerResponse> payers { get; set; }
        public List<SharedWithResponse> sharedwiths { get; set; }
        public List<SettlementResponse> settlements { get; set; }
    }

}
