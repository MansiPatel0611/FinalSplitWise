using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class Group
    {
        [Key]
        public int groupid { get; set; }
        public string group_name { get; set; }

        public int group_created_by { get; set; }
        public User user { get; set; }

        public bool is_simplified_depts { get; set; }

        [ForeignKey("groupId")]
        public ICollection<GroupMember> gm_group_id { get; set; }

        [ForeignKey("groupId")]
        public ICollection<Bill> bill_group_id { get; set; }

        [ForeignKey("groupId")]
        public ICollection<Settlements> settlement_group_id { get; set; }

        [ForeignKey("groupId")]
        public ICollection<Transaction> transaction_group_id { get; set; }
    }
}
