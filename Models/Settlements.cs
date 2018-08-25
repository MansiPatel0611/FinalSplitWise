using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class Settlements
    {
        [Key]
        public int settlementid { get; set; }

        public int payerId { get; set; }
        public User payer { get; set; }

        public int payeeId { get; set; }
        public User payee { get; set; }

        public double amount { get; set; }
        //public DateTime paid_on { get; set; }

        //public int billId { get; set; }
        //public Bill bill { get; set; }

        public int? groupId { get; set; }
        public Group group { get; set; }
    }
}
