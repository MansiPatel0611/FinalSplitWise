using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalSplitWise.Models;
using FinalSplitWise.Repositories;
using FinalSplitWise.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalSplitWise.Apis
{
    [Produces("application/json")]
    public class GroupMemberController : Controller
    {
        GroupMemberData _groupMemberData;
        ILogger _Logger;

        public GroupMemberController(GroupMemberData groupMemberData, ILoggerFactory loggerFactory)
        {
            _groupMemberData = groupMemberData;
            _Logger = loggerFactory.CreateLogger(nameof(GroupMemberController));
        }


        [Route("api/GroupMember/addMember/{user}/{group}")]
        [HttpPost("{user}/{group}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AddMember(int user, int group)
        {
            try
            {
                var newmember =await _groupMemberData.AddGroupMemberAsync(user, group);
                if (newmember == null || newmember.memberid<=0)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(new CommonResponse { Status = true });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/GroupMember/deleteMember/{user}/{group}")]
        [HttpDelete("{user}/{group}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> DeleteMember(int user,int group)
        {
            try
            {
                var status = await _groupMemberData.DeleteGroupMemberAsync(user, group);
                if (!status)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(new CommonResponse { Status = true });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });

            }
        }
    }
}
