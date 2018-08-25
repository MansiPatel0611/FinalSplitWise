using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface GroupMemberData
    {
        Task<GroupMember> AddGroupMemberAsync(int group,int user);
        Task<bool> DeleteGroupMemberAsync(int user, int group);
    }
}
