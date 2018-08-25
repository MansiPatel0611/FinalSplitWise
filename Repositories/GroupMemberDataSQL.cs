using FinalSplitWise.Data;
using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public class GroupMemberDataSQL:GroupMemberData
    {
        private readonly SystemDBContext _Context;
        private readonly ILogger _Logger;

        public GroupMemberDataSQL(SystemDBContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("GroupMembersRepository");
        }


        public async Task<GroupMember> AddGroupMemberAsync( int user, int group)
        {
            GroupMember groupMember = new GroupMember();
            groupMember.groupId = group;
            groupMember.userId = user;
            var member = _Context.groupMembers.SingleOrDefault(c => c.userId == user && c.groupId == group);
            if(member==null)
            {
                _Context.groupMembers.Add(groupMember);
            }
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(AddGroupMemberAsync)}: " + exp.Message);
            }

            return groupMember;
        }

        public async Task<bool> DeleteGroupMemberAsync(int user, int group)
        {
            var member = await _Context.groupMembers
                          .SingleOrDefaultAsync(c=>c.groupId==group && c.userId==user);
            _Context.groupMembers.Remove(member);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteGroupMemberAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
