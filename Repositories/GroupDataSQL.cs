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
    public class GroupDataSQL: GroupData
    {
        private readonly SystemDBContext _Context;
        private readonly ILogger _Logger;

        public GroupDataSQL(SystemDBContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("GroupsRepository");
        }

        public GroupResponse ModelData(Group group)
        {
            GroupResponse response = new GroupResponse();
            response.groupid = group.groupid;

            var nameid = _Context.users.SingleOrDefault(c => c.userid == group.group_created_by);
            var detail = new Detail();
            detail.id = nameid.userid;
            detail.name = nameid.user_name;

            response.group_created_by = detail;

            response.group_name = group.group_name;

            response.is_simplified_depts = group.is_simplified_depts;

            var users = new List<Detail>();
            var data = _Context.groupMembers.Where(c => c.groupId == group.groupid).ToList();
            for (var i = 0; i < data.Count; i++)
            {
                var x = _Context.users.SingleOrDefault(c => c.userid == data[i].userId);
                var detail1 = new Detail();
                detail1.id = x.userid;
                detail1.name = x.user_name;
                users.Add(detail1);
            }
            response.memberLists = users;

            return response;
        }

        public async Task<GroupResponse> CreateGroupAsync(CreateGroupResponse group1)
        {
            GroupResponse response = new GroupResponse();
            Group group = new Group();
            group.group_name = group1.group_name;
            group.group_created_by = group1.group_created_by;
            group.is_simplified_depts = group1.is_simplified_depts;
            _Context.groups.Add(group);
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(CreateGroupAsync)}: " + exp.Message);
            }

            foreach (var x in group1.groupMembers)
            {
                GroupMember groupMember = new GroupMember();
                groupMember.groupId = group.groupid;
                groupMember.userId = x.userId;
                _Context.groupMembers.Add(groupMember);

                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(CreateGroupAsync)}: " + exp.Message);
                }
            }
            response = ModelData(group);
            return response;
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var user = await _Context.groups
                          .SingleOrDefaultAsync(c => c.groupid == id);
            _Context.groups.Remove(user);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteGroupAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<GroupResponse> GetGroupAsync(int groupid)
        {
            GroupResponse GroupResponse = new GroupResponse();

            var group= await _Context.groups
               .SingleOrDefaultAsync(c => c.groupid== groupid);
            GroupResponse = ModelData(group);
            return GroupResponse;
        }

        public async Task<List<GroupResponse>> GetGroupsAsync(int userid)
        {
            var groups = new List<GroupResponse>();

            var group = await _Context.groups
               .Where(c => c.gm_group_id.Any(gs => gs.userId == userid)).ToListAsync();

            for (var j = 0; j < group.Count; j++)
            {
                var GroupResponse= new GroupResponse();
                GroupResponse = ModelData(group[j]);
                groups.Add(GroupResponse);
            }
            return groups;

        }

        public async Task<bool> UpdateGroupAsync(Group group)
        {
            _Context.groups.Attach(group);
            _Context.Entry(group).State = EntityState.Modified;
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateGroupAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
