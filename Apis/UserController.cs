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
    public class UserController : Controller
    {
        UserData _userdata;
        ILogger _Logger;

        public UserController(UserData userdata, ILoggerFactory loggerFactory)
        {
            _userdata = userdata;
            _Logger = loggerFactory.CreateLogger(nameof(UserController));
        }

        //[Route("api/User/getUsers")]
        //[HttpGet]
        //[ProducesResponseType(typeof(List<UserResponse>), 200)]
        //[ProducesResponseType(typeof(CommonResponse), 400)]
        //public async Task<ActionResult> Users()
        //{
        //    try
        //    {
        //        var users = await _userdata
        //            .GetUsersAsync();
        //        return Ok(users);
        //    }
        //    catch (Exception exp)
        //    {
        //        _Logger.LogError(exp.Message);
        //        return BadRequest(new CommonResponse { Status = false });
        //    }
        //}

        [Route("api/User/getUser/{id}")]
        [HttpGet("{id}", Name = "GetUserRoute")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Users(int id)
        {
            try
            {
                var user = await _userdata.GetUserAsync(id);
                //var user = await _userdata.getUserModelAsync(id);
                return Ok(user);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/User/login/{email}/{password}")]
        [HttpGet("{email}/{password}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Users(string email, string password)
        {
            try
            {
                var user = await _userdata.LoginAsync(email, password);
                if(user!=null)
                return Ok(new CommonResponse { Status = true,id=user.userid });
                else
                    return BadRequest(new CommonResponse { Status = false });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/User/Register/{name}/{email}/{password}")]
        [HttpPost("{name}/{email}/{password}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AddUser(string name, string email, string password)
        {
            try
            {
                var user = await _userdata.RegisterAsync(name, email, password);
                if (user == null || user.userid==0)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return CreatedAtRoute("GetUserRoute", new { id = user.userid }, new CommonResponse { Status = true, id = user.userid });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        //[Route("api/User/addUser")]
        //[HttpPost]
        //[ProducesResponseType(typeof(CommonResponse), 201)]
        //[ProducesResponseType(typeof(CommonResponse), 400)]
        //public async Task<ActionResult> AddUser([FromBody]User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new CommonResponse { Status = false });
        //    }

        //    try
        //    {
        //        var newUser = await _userdata.InsertUserAsync(user);
        //        if (newUser == null)
        //        {
        //            return BadRequest(new CommonResponse { Status = false });
        //        }
        //        return CreatedAtRoute("GetUserRoute", new { id = newUser.userid }, new CommonResponse { Status = true, id = newUser.userid });

        //    }
        //    catch (Exception exp)
        //    {
        //        _Logger.LogError(exp.Message);
        //        return BadRequest(new CommonResponse { Status = false });
        //    }
        //}

        [Route("api/User/updateUser/{id}")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }

            try
            {
                var status = await _userdata.UpdateUserAsync(user);
                if (!status)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(new UserResponse
                {
                    userid = user.userid,
                    user_name = user.user_name,
                    password = user.password,
                    email_id = user.email_id,
                    phone_no = user.phone_no
                });
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/User/deleteUser/{id}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var status = await _userdata.DeleteUserAsync(id);
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