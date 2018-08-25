using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class CreateGroupResponse
    {
        public int groupid { get; set; }

        public string group_name { get; set; }

        public int group_created_by { get; set; }

        public bool is_simplified_depts { get; set; }

        public List<GroupMemberResponse> groupMembers { get; set; }
    }
}
