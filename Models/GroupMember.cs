using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class GroupMember
    {
        [Key]
        public int memberid { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

        public int groupId { get; set; }
        public Group group { get; set; }
    }
}
