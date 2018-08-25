using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface BillData
    {
        Task<List<BillGetResponse>> GetGroupBillsAsync(int groupid);
        Task<List<BillGetResponse>> GetFriendBillsAsync(int userid,int friendid);
        Task<List<BillGetResponse>> GetAllBillsAsync(int userid);
        Task<Bill> AddNewBillAsync(BillResponse billResponse);
        Task<bool> DeleteBillAsync(int id);
        Task<bool> UpdateBillAsync(BillResponse billResponse);
        Task<BillGetResponse> GetBillAsync(int billid);
    }
}
