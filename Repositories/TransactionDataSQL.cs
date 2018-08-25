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
    public class TransactionDataSQL:TransactionData
    {
        private readonly SystemDBContext _Context;
        private readonly ILogger _Logger;

        public TransactionDataSQL(SystemDBContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("TransactionsRepository");
        }

        public async Task<List<TransactionGetResponse>> GetAll(int userid)
        {
            var transactions = new List<TransactionGetResponse>();

            var trans = _Context.transactions
                .Where(c => (c.payerId == userid || c.payeeId == userid) || c.group.gm_group_id
                .Any(x => x.userId == userid)).ToList();
            for (int i = 0; i < trans.Count; i++)
            {
                TransactionGetResponse transaction = new TransactionGetResponse();
                if (trans[i].groupId != null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == trans[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    transaction.groupId = detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == trans[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                transaction.payerId = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == trans[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                transaction.payeeId = detail;

                transaction.paid_on = trans[i].paid_on;
                transaction.paid_amount = trans[i].paid_amount;
                transactions.Add(transaction);
            }
            return transactions;
        }

        public async Task<List<TransactionGetResponse>> GetAllAmongFriend(int userid, int friendid)
        {
            var transactions = new List<TransactionGetResponse>();

            var trans = _Context.transactions
                .Where(c => (c.payerId == userid && c.payeeId == friendid)
                || (c.payerId == friendid && c.payeeId == userid)).ToList();
            for(int i=0;i<trans.Count;i++)
            {
                TransactionGetResponse transaction = new TransactionGetResponse();
                if(trans[i].groupId!=null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == trans[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    transaction.groupId = detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == trans[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                transaction.payerId = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == trans[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                transaction.payeeId = detail;

                transaction.paid_on = trans[i].paid_on;
                transaction.paid_amount = trans[i].paid_amount;
                transactions.Add(transaction);
            }
            return transactions;
        }

        public async Task<List<TransactionGetResponse>> GetAllAmongGroup(int groupid)
        {
            var transactions = new List<TransactionGetResponse>();

            var trans = _Context.transactions
                .Where(c =>c.groupId==groupid).ToList();
            for (int i = 0; i < trans.Count; i++)
            {
                TransactionGetResponse transaction = new TransactionGetResponse();
                if (trans[i].groupId != null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == trans[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    transaction.groupId = detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == trans[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                transaction.payerId = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == trans[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                transaction.payeeId = detail;

                transaction.paid_on = trans[i].paid_on;
                transaction.paid_amount = trans[i].paid_amount;
                transactions.Add(transaction);
            }
            return transactions;
        }

        public async Task<List<GetSettlementResponse>> getFriendSettlement(int userid, int friendid)
        {
            var settlements = new List<GetSettlementResponse>();
            var sett = _Context.settlements.Where(c => (c.payerId == userid && c.payeeId == friendid)
                || (c.payerId == friendid && c.payeeId == userid)
                ).ToList();
            for (int i = 0; i < sett.Count; i++)
            {
                GetSettlementResponse getSettlement = new GetSettlementResponse();
                if (sett[i].groupId != null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == sett[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    getSettlement.group = detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == sett[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                getSettlement.payer = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == sett[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                getSettlement.payee = detail;

                getSettlement.amount = sett[i].amount;
                settlements.Add(getSettlement);
            }
            return settlements;
        }

        public async Task<List<GetSettlementResponse>> getGroupSettlement(int userid, int groupid)
        {
            var settlements = new List<GetSettlementResponse>();
            var sett = _Context.settlements.Where(c => (c.groupId == groupid)
                ).ToList();
            for (int i = 0; i < sett.Count; i++)
            {
                GetSettlementResponse getSettlement = new GetSettlementResponse();
                if (sett[i].groupId != null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == sett[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    getSettlement.group = detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == sett[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                getSettlement.payer = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == sett[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                getSettlement.payee = detail;

                getSettlement.amount = sett[i].amount;
                settlements.Add(getSettlement);
            }
            return settlements;
        }

        public async Task<List<GetSettlementResponse>> getSettlements()
        {
            var settlements = new List<GetSettlementResponse>();
            var sett = _Context.settlements.ToList();
            for(int i=0;i<sett.Count;i++)
            {
                GetSettlementResponse getSettlement = new GetSettlementResponse();
                if(sett[i].groupId!=null)
                {
                    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == sett[i].groupId);
                    var detail2 = new Detail();
                    detail2.id = nameid2.groupid;
                    detail2.name = nameid2.group_name;
                    getSettlement.group= detail2;
                }

                var detail1 = new Detail();
                var nameid1 = _Context.users.SingleOrDefault(c => c.userid == sett[i].payerId);
                detail1.name = nameid1.user_name;
                detail1.id = nameid1.userid;
                getSettlement.payer = detail1;

                var detail = new Detail();
                var nameid = _Context.users.SingleOrDefault(c => c.userid == sett[i].payeeId);
                detail.name = nameid.user_name;
                detail.id = nameid.userid;
                getSettlement.payee = detail;

                getSettlement.amount = sett[i].amount;
                settlements.Add(getSettlement);
            }
            return settlements;
        }

        public async Task<bool> SettleUpAsync(TransactionResponse transaction)
        {
            Transaction transactions = new Transaction();
            if(transaction.groupId!=0)
            {
                transactions.groupId = transaction.groupId;
            }
            transactions.paid_amount = transaction.paid_amount;
            transactions.payeeId = transaction.payeeId;
            transactions.payerId = transaction.payerId;
            transactions.paid_on = transaction.paid_on;
            _Context.transactions.Add(transactions);
            //await _Context.SaveChangesAsync();

                var settle =await _Context.settlements.FirstOrDefaultAsync(c =>
              c.payeeId == transactions.payeeId && c.payerId == transactions.payerId
              && c.groupId == transactions.groupId);
            if (settle != null)
            {
                settle.amount = settle.amount - transactions.paid_amount;
                _Context.settlements.Attach(settle);
            }
            else
            {
                if(transaction.groupId!=0)
                {
                    Settlements settlements = new Settlements();
                    settlements.payerId = transactions.payeeId;
                    settlements.payeeId = transactions.payerId;
                    settlements.amount = transactions.paid_amount;
                    settlements.groupId = transactions.groupId;

                    var settle1 = await _Context.settlements.FirstOrDefaultAsync(c =>
                    c.payeeId == settlements.payeeId && c.payerId == settlements.payerId);
                    if (settle1 != null)
                    {
                        settle1.amount = settle1.amount + transactions.paid_amount;
                        _Context.settlements.Attach(settle1);
                    }
                    else
                    {
                        _Context.settlements.Add(settlements);
                    }
                }
                else
                {
                    Settlements settlements = new Settlements();
                    settlements.payerId = transactions.payeeId;
                    settlements.payeeId = transactions.payerId;
                    settlements.amount = transactions.paid_amount;

                    var settle1 = await _Context.settlements.FirstOrDefaultAsync(c =>
                    c.payeeId == settlements.payeeId && c.payerId == settlements.payerId);
                    if (settle1 != null)
                    {
                        settle1.amount = settle1.amount + transactions.paid_amount;
                        _Context.settlements.Attach(settle1);
                    }
                    else
                    {
                        _Context.settlements.Add(settlements);
                    }
                }
            }
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(SettleUpAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
