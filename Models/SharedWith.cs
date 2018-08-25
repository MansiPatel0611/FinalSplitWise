using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class SharedWith
    {
        [Key]
        public int sharedid { get; set; }

        public int shared_withId { get; set; }
        public User shared_with { get; set; }

        public double owes_amount { get; set; }


        public int billId { get; set; }
        public Bill bill { get; set; }

        //public int owes_toId { get; set; }
        //public User owes_to { get; set; }
    }
}
