using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalSplitWise.ResponseModel
{
    public class UserResponse
    {
        public int userid { get; set; }
        public string user_name { get; set; }
        public string email_id { get; set; }
        public string password { get; set; }
        public string phone_no { get; set; }

       // public bool Status { get; set; }
        //public ModelStateDictionary ModelState { get; internal set; }
    }
}
