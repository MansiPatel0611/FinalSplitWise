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
    public class GroupController : Controller
    {
        GroupData _groupdata;
        ILogger _Logger;

        public GroupController(GroupData groupData, ILoggerFactory loggerFactory)
        {
            _groupdata = groupData;
            _Logger = loggerFactory.CreateLogger(nameof(GroupController));
        }

        [Route("api/Group/getGroups/{id}")]
        [HttpGet("{id}", Name = "GetGroupRoute")]
        [ProducesResponseType(typeof(GroupResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Groups(int id)
        {
            try
            {
                var groups = await _groupdata.GetGroupsAsync(id);
                //var user = await _userdata.getUserModelAsync(id);
                return Ok(groups);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Group/getGroupData/{groupid}")]
        [HttpGet("{groupid}")]
        [ProducesResponseType(typeof(GroupResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> GroupDetails(int groupid)
        {
            try
            {
                var groups = await _groupdata.GetGroupAsync(groupid);
                return Ok(groups);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Group/addGroup")]
        [HttpPost]
        [ProducesResponseType(typeof(GroupResponse), 201)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AddGroup([FromBody]CreateGroupResponse group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }

            try
            {
                var newgroup = await _groupdata.CreateGroupAsync(group);
                if (newgroup == null)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(newgroup); 
                    //CreatedAtRoute("GetGroupRoute", new { id = newgroup.groupid }, new CommonResponse { Status = true, id = newgroup.groupid});

            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Group/updateGroup/{id}")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody]Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }

            try
            {
                var status = await _groupdata.UpdateGroupAsync(group);
                if (!status)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(new CommonResponse { Status = true ,id=group.groupid });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Group/deleteGroup/{id}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            try
            {
                var status = await _groupdata.DeleteGroupAsync(id);
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
