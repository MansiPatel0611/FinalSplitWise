using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class Bill
    {
        [Key]
        public int billid { get; set; }
        public string description { get; set; }
        public double total_amount { get; set; }

        public int bill_created_byId { get; set; }
        public User bill_created_by { get; set; }

        public int bill_updated_byId { get; set; }
        public User bill_updated_by { get; set; }

        public DateTime bill_date { get; set; }
        public DateTime bill_created_at { get; set; }
        public DateTime bill_updated_at { get; set; }

        public int? groupId { get; set; }
        public Group group { get; set; }

        [ForeignKey("billId")]
        public ICollection<Payer> bill_payer { get; set; }

        [ForeignKey("billId")]
        public ICollection<SharedWith> bill_shared_with { get; set; }

        //[ForeignKey("billId")]
        //public ICollection<Settlements> bill_settlement { get; set; }
    }
}
