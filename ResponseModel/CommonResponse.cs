using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class CommonResponse
    {
        public bool Status { get; set; }
        public int id { get; set; }
        //public ModelStateDictionary ModelState { get; internal set; }
    }
}
