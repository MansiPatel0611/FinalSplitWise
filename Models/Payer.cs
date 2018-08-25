using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class Payer
    {
        public int payerid { get; set; }

        public int paid_byId { get; set; }
        public User paid_by { get; set; }

        public double amount_paid { get; set; }

        public int billId { get; set; }
        public Bill bill { get; set; }
    }
}
