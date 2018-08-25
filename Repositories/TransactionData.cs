using FinalSplitWise.Models;
using FinalSplitWise.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Repositories
{
    public interface TransactionData
    {
        Task<bool> SettleUpAsync(TransactionResponse transaction);
        Task<List<GetSettlementResponse>> getSettlements();
        Task<List<GetSettlementResponse>> getFriendSettlement(int userid, int friendid);
        Task<List<GetSettlementResponse>> getGroupSettlement(int userid, int groupid);
        Task<List<TransactionGetResponse>> GetAllAmongFriend(int userid,int friendid);
        Task<List<TransactionGetResponse>> GetAllAmongGroup(int groupid);
        Task<List<TransactionGetResponse>> GetAll(int userid);

    }
}
