using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalSplitWise.Repositories;
using FinalSplitWise.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalSplitWise.Apis
{
    [Produces("application/json")]
    public class FriendController : Controller
    {
        FriendData _frienddata;
        ILogger _Logger;

        public FriendController(FriendData friendData, ILoggerFactory loggerFactory)
        {
            _frienddata = friendData;
            _Logger = loggerFactory.CreateLogger(nameof(FriendController));
        }

        [Route("api/Friend/getFriend/{id}")]
        [HttpGet("{id}", Name = "GetFriendRoute")]
        [ProducesResponseType(typeof(FriendResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Users(int id)
        {
            try
            {
                var user = await _frienddata.GetFriendsAsync(id);
                //var user = await _userdata.getUserModelAsync(id);
                return Ok(user);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Friend/addFriend/{userid}/{name}/{email}")]
        [HttpPost("{userid}/{name}/{email}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AddFriend(int userid,string name, string email)
        {
            try
            {
                var friend = await _frienddata.AddNewFriendAsync(name, email, userid);
                if (friend == null || friend.id==0)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok( new CommonResponse { Status = true });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }
        [Route("api/Friend/removeFriend/{userid}/{friendid}")]
        [HttpDelete("{userid}/{friendid}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> DeleteUser(int userid,int friendid)
        {
            try
            {
                var status = await _frienddata.RemoveFriendAsync(userid, friendid);
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
