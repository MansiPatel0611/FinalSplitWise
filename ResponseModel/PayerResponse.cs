using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class PayerResponse
    {
        public int paid_byId { get; set; }
        public double amount_paid { get; set; }
        public int billId { get; set; }
    }
}
