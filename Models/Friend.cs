using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Models
{
    public class Friend
    {
        [Key]
        public int id { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

        public int friendId { get; set; }
        public User friend { get; set; }
        
    }
}
