using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface FriendData
    {
        Task<List<FriendResponse>> GetFriendsAsync(int user_id);
        Task<Friend> AddNewFriendAsync(string name, string email,int userid);
        Task<bool> RemoveFriendAsync(int userid,int friendid);
    }
}
