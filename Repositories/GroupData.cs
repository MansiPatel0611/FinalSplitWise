using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface GroupData
    {
        Task<List<GroupResponse>> GetGroupsAsync(int userid);

        Task<GroupResponse> GetGroupAsync(int groupid);

        Task<GroupResponse> CreateGroupAsync(CreateGroupResponse group);

        Task<bool> UpdateGroupAsync(Group group);

        Task<bool> DeleteGroupAsync(int id);
    }
}
