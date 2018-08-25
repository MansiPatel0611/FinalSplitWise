using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class GroupResponse
    {
        public int groupid { get; set; }
        public string group_name { get; set; }

        public Detail group_created_by { get; set; }

        public bool is_simplified_depts { get; set; }
        public List<Detail> memberLists { get; set; }
    }
    public class Detail
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
