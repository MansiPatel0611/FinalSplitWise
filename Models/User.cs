using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        public string user_name { get; set; }
        public string email_id { get; set; }
        public string password { get; set; }
        public string phone_no { get; set; }

        [InverseProperty("user")]
        public ICollection<Friend> user_id { get; set; }
        [InverseProperty("friend")]
        public ICollection<Friend> friend_id { get; set; }

        [ForeignKey("group_created_by")]
        public ICollection<Group> group_created_by { get; set; }

        [ForeignKey("userId")]
        public ICollection<GroupMember> gm_user_id { get; set; }

        [InverseProperty("bill_created_by")]
        public ICollection<Bill> bill_created_by { get; set; }
        [InverseProperty("bill_updated_by")]
        public ICollection<Bill> bill_updated_by { get; set; }

        [ForeignKey("paid_byId")]
        public ICollection<Payer> paid_by_id { get; set; }

        [InverseProperty("shared_with")]
        public ICollection<SharedWith> shared_with { get; set; }
        //[InverseProperty("owes_to")]
        //public ICollection<SharedWith> owes_to { get; set; }

        [InverseProperty("payer")]
        public ICollection<Settlements> payer { get; set; }
        [InverseProperty("payee")]
        public ICollection<Settlements> payee { get; set; }

        [InverseProperty("payer")]
        public ICollection<Transaction> transpayer { get; set; }
        [InverseProperty("payee")]
        public ICollection<Transaction> transpayee { get; set; }
    }
}
