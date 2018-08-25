using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface UserData
    {
      //  Task<User> InsertUserAsync(User user);

        //Task<List<UserResponse>> GetUsersAsync();

        Task<UserResponse> GetUserAsync(int id);

        Task<User> LoginAsync(string email, string password);

        Task<User> RegisterAsync(string name, string email, string password);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
    }
}
