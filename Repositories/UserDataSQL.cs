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
    public class UserDataSQL : UserData
    {
        private readonly SystemDBContext _Context;
        private readonly ILogger _Logger;

        public UserDataSQL(SystemDBContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("UsersRepository");
        }

        public async Task<UserResponse> GetUserAsync(int id)
        {
            var user =await _Context.users
                .SingleOrDefaultAsync(c => c.userid == id);

            UserResponse model = new UserResponse();
            model.userid = user.userid;
            model.user_name = user.user_name;
            model.phone_no = user.phone_no;
            model.email_id = user.email_id;
            model.password = user.password;
            return model;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _Context.users.SingleOrDefaultAsync(
                c => (c.email_id == email)
                &&
                (c.password == password)
                );
            //CommonResponse model = new CommonResponse();
            //model.id = user.userid;
            //model.Status = true;
            //return model;

        }

        //public async Task<List<UserResponse>> GetUsersAsync()
        //{
        //    return await _Context.users
        //          .Select(x => new UserResponse
        //          {
        //              userid = x.userid,
        //              password = x.password,
        //              email_id = x.email_id,
        //              phone_no=x.phone_no,
        //              user_name=x.user_name
        //          }
        //       ).ToListAsync();
        //}

        //public async Task<User> InsertUserAsync(User user)
        //{
        //    _Context.users.Add(user);
        //    try
        //    {
        //        await _Context.SaveChangesAsync();
        //    }
        //    catch (Exception exp)
        //    {
        //        _Logger.LogError($"Error in {nameof(InsertUserAsync)}: " + exp.Message);
        //    }

        //    return user;
        //}

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            User user = new User();
            var userexist=_Context.users.Where(c=>c.email_id==email).SingleOrDefault();
            if (userexist == null)
            {
                user.user_name = name;
                user.password = password;
                user.email_id = email;
                _Context.users.Add(user);
                try
                {
                    await _Context.SaveChangesAsync();
                }

                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(RegisterAsync)}: " + exp.Message);
                }
            }
            else
            {
                _Logger.LogError($"Error in {nameof(RegisterAsync)}");
            }
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _Context.users.Attach(user);
            _Context.Entry(user).State = EntityState.Modified;
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateUserAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _Context.users
                          .SingleOrDefaultAsync(c => c.userid == id);
            _Context.users.Remove(user);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteUserAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
