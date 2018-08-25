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
    public class BillDataSQL: BillData
    {
        private readonly SystemDBContext _Context;
        private readonly ILogger _Logger;

        public BillDataSQL(SystemDBContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("BillsRepository");
        }

        public async Task<Bill> AddNewBillAsync(BillResponse billResponse)
        {
            Bill bill = new Bill();

            if (billResponse.groupId != 0)
            {
                bill.groupId = billResponse.groupId;
            }
                bill.bill_created_at = billResponse.bill_created_at;
                bill.bill_created_byId = billResponse.bill_created_byId;
                bill.bill_date = billResponse.bill_date;
                bill.bill_updated_at = billResponse.bill_updated_at;
                bill.description = billResponse.description;
                bill.total_amount = billResponse.total_amount;
                bill.bill_updated_byId = billResponse.bill_updated_byId;
                _Context.bills.Add(bill);
                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(AddNewBillAsync)}: " + exp.Message);
                }
               

            //int id = bill.billid;
            foreach(var x in billResponse.payers)
            {
                Payer payer = new Payer();
                payer.billId = bill.billid;
                payer.paid_byId = x.paid_byId;
                payer.amount_paid = x.amount_paid;
                _Context.payers.Add(payer);

                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(AddNewBillAsync)}: " + exp.Message);
                }
            }

            foreach (var x in billResponse.sharedwiths)
            {
                SharedWith sharedWith = new SharedWith();
                sharedWith.billId = bill.billid;
                sharedWith.shared_withId = x.shared_withId;
                sharedWith.owes_amount = x.owes_amount;
                _Context.sharedWiths.Add(sharedWith);

                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(AddNewBillAsync)}: " + exp.Message);
                }
            }

            foreach (var x in billResponse.settlements)
            {
                if (billResponse.groupId!=0)
                {
                    Settlements settlements = new Settlements();
                    settlements = await _Context.settlements
                       .SingleOrDefaultAsync(c => c.groupId == billResponse.groupId
                       && c.payeeId == x.payeeId && c.payerId == x.payerId);
                    if (settlements != null)
                    {
                        settlements.payerId = x.payerId;
                        settlements.payeeId = x.payeeId;
                        settlements.amount = settlements.amount + x.paid_amount;
                        settlements.groupId = billResponse.groupId;
                        _Context.settlements.Attach(settlements);
                        _Context.Entry(settlements).State = EntityState.Modified;
                    }
                    else
                    {
                        settlements = await _Context.settlements
                       .SingleOrDefaultAsync(c => c.groupId == billResponse.groupId
                       && c.payeeId == x.payerId && c.payerId == x.payeeId);
                        if (settlements != null)
                        {
                            settlements.payerId = x.payeeId;
                            settlements.payeeId = x.payerId;
                            settlements.amount = settlements.amount - x.paid_amount;
                            settlements.groupId = billResponse.groupId;
                            _Context.settlements.Attach(settlements);
                            _Context.Entry(settlements).State = EntityState.Modified;
                        }
                        else
                        {
                            Settlements settlements1 = new Settlements();
                            settlements1.payerId = x.payerId;
                            settlements1.payeeId = x.payeeId;
                            settlements1.amount = x.paid_amount;
                            settlements1.groupId = billResponse.groupId;
                            _Context.settlements.Add(settlements1);
                        }
                       
                    }
                }
                else
                {
                    Settlements settlements = new Settlements();
                    settlements = await _Context.settlements
                       .FirstOrDefaultAsync(c => c.groupId == bill.groupId &&
                       c.payeeId == x.payeeId && c.payerId == x.payerId);
                    if (settlements != null)
                    {
                        settlements.payerId = x.payerId;
                        settlements.payeeId = x.payeeId;
                        settlements.amount = settlements.amount + x.paid_amount;
                        _Context.settlements.Attach(settlements);
                        _Context.Entry(settlements).State = EntityState.Modified;
                    }
                    else
                    {
                        settlements = await _Context.settlements
                      .FirstOrDefaultAsync(c => c.groupId == bill.groupId &&
                      c.payeeId == x.payerId && c.payerId == x.payeeId);

                        if (settlements != null)
                        {
                            settlements.payerId = x.payeeId;
                            settlements.payeeId = x.payerId;
                            settlements.amount = settlements.amount - x.paid_amount;
                            _Context.settlements.Attach(settlements);
                            _Context.Entry(settlements).State = EntityState.Modified;
                        }
                        else
                        {
                            Settlements settlements1 = new Settlements();
                            settlements1.payerId = x.payerId;
                            settlements1.payeeId = x.payeeId;
                            settlements1.amount = x.paid_amount;
                            //  settlements1.groupId = billResponse.groupId;
                            _Context.settlements.Add(settlements1);
                        }
                       
                    }
                }
                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception exp)
                {
                    _Logger.LogError($"Error in {nameof(AddNewBillAsync)}: " + exp.Message);
                }
            }

            return bill;
        }
        
        public async Task<bool> DeleteBillAsync(int id)
        {
            var bill = await _Context.bills
                         .SingleOrDefaultAsync(c => c.billid == id);
            _Context.bills.Remove(bill);
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(DeleteBillAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<List<BillGetResponse>> GetAllBillsAsync(int userid)
        {
            var billlist = new List<BillGetResponse>();
            var bills = _Context.bills.Where(
                c=>c.bill_created_byId==userid || c.bill_payer.Any(
                    x=>x.paid_byId==userid) || c.bill_shared_with.Any(
                        x=>x.shared_withId==userid)).ToList();

            for (var j = 0; j < bills.Count; j++)
            {
                BillGetResponse bill = new BillGetResponse();
                bill = await GetBillAsync(bills[j].billid);

                //bill.billid = bills[j].billid;
                //bill.bill_created_at = bills[j].bill_created_at;

                //var detail = new Detail();
                //var nameid = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_created_byId);
                //detail.name = nameid.user_name;
                //detail.id = nameid.userid;

                //bill.bill_created_by = detail;

                //var detail1 = new Detail();
                //var nameid1 = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_updated_byId);
                //detail1.name = nameid1.user_name;
                //detail1.id = nameid1.userid;

                //bill.bill_updated_by = detail1;

                //var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == bills[j].groupId);
                //var detail2 = new Detail();
                //detail2.id = nameid2.groupid;
                //detail2.name = nameid2.group_name;
                //bill.group = detail2;

                //bill.bill_date = bills[j].bill_date;
                //bill.bill_updated_at = bills[j].bill_updated_at;
                //bill.description = bills[j].description;
                //bill.total_amount = bills[j].total_amount;

                //var x = _Context.users.Where(c => c.paid_by_id.Any(y => y.billId == bills[j].billid)).ToList();
                //var pay = new List<BillDetail>();
                //for (int i = 0; i < x.Count; i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == x[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.payers.SingleOrDefault(c => c.billId == bills[j].billid && c.paid_byId == x[i].userid);
                //    billDetail.amount = amtid.amount_paid;
                //    pay.Add(billDetail);
                //}
                //bill.payers = pay;

                //var p = _Context.users.Where(c => c.shared_with.Any(y => y.billId == bills[j].billid)).ToList();
                //var share = new List<BillDetail>();
                //for (int i = 0; i < p.Count; i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == p[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.sharedWiths.SingleOrDefault(c =>
                //    c.billId == bills[j].billid && c.shared_withId == p[i].userid);
                //    billDetail.amount = amtid.owes_amount;
                //    share.Add(billDetail);
                //}
                //bill.sharedwiths = share;

                billlist.Add(bill);
            }
            return billlist;
        }

        public async Task<BillGetResponse> GetBillAsync(int billid)
        {
            BillGetResponse bill = new BillGetResponse();
            var bills = _Context.bills.SingleOrDefault(c => c.billid == billid);

            if (bills.groupId != null)
            {
                var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == bills.groupId);
                var detail2 = new Detail();
                detail2.id = nameid2.groupid;
                detail2.name = nameid2.group_name;
                bill.group = detail2;
            }
            bill.billid = bills.billid;
            bill.bill_created_at = bills.bill_created_at;

            var detail = new Detail();
            var nameid = _Context.users.SingleOrDefault(c => c.userid == bills.bill_created_byId);
            detail.name = nameid.user_name;
            detail.id = nameid.userid;

            bill.bill_created_by = detail;

            var detail1 = new Detail();
            var nameid1 = _Context.users.SingleOrDefault(c => c.userid == bills.bill_updated_byId);
            detail1.name = nameid1.user_name;
            detail1.id = nameid1.userid;

            bill.bill_updated_by = detail1;

            var x = _Context.users.Where(c => c.paid_by_id.Any(y => y.billId == bills.billid)).ToList();
            var pay = new List<BillDetail>();
            for (int i = 0; i < x.Count; i++)
            {
                BillDetail billDetail = new BillDetail();
                var y = _Context.users.SingleOrDefault(c => c.userid == x[i].userid);
                billDetail.id = y.userid;
                billDetail.name = y.user_name;
                var amtid = _Context.payers.SingleOrDefault(c => 
                c.billId == bills.billid && c.paid_byId == x[i].userid);
                billDetail.amount = amtid.amount_paid;
                pay.Add(billDetail);
            }
            bill.payers = pay;

            var p = _Context.users.Where(c => c.shared_with.Any(y => y.billId == bills.billid)).ToList();
            var share = new List<BillDetail>();
            for (int i = 0; i < p.Count; i++)
            {
                BillDetail billDetail = new BillDetail();
                var y = _Context.users.SingleOrDefault(c => c.userid == p[i].userid);
                billDetail.id = y.userid;
                billDetail.name = y.user_name;
                var amtid = _Context.sharedWiths.SingleOrDefault(c =>
                c.billId == bills.billid && c.shared_withId == p[i].userid);
                billDetail.amount = amtid.owes_amount;
                share.Add(billDetail);
            }
            bill.sharedwiths = share;

            bill.bill_date = bills.bill_date;
            bill.bill_updated_at = bills.bill_updated_at;
            bill.description = bills.description;
            bill.total_amount = bills.total_amount;
            return bill;
        }

        public async Task<List<BillGetResponse>> GetFriendBillsAsync(int userid, int friendid)
        {
            var billlist = new List<BillGetResponse>();

            var bills = _Context.bills.Where(c =>
            c.bill_payer.Any(x=>x.paid_byId==userid || x.paid_byId ==friendid) &&
            c.bill_shared_with.Any(y=>y.shared_withId==userid || y.shared_withId==friendid)).ToList();

            for (var j = 0; j < bills.Count; j++)
            {
                BillGetResponse bill = new BillGetResponse();
                bill = await GetBillAsync(bills[j].billid);
                //if (bills[j].groupId!=null)
                //{
                //    var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == bills[j].groupId);
                //    var detail2 = new Detail();
                //    detail2.id = nameid2.groupid;
                //    detail2.name = nameid2.group_name;
                //    bill.group = detail2;
                //}
                //    bill.billid = bills[j].billid;
                //    bill.bill_created_at = bills[j].bill_created_at;

                //    var detail = new Detail();
                //    var nameid = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_created_byId);
                //    detail.name = nameid.user_name;
                //    detail.id = nameid.userid;

                //    bill.bill_created_by = detail;

                //    var detail1 = new Detail();
                //    var nameid1 = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_updated_byId);
                //    detail1.name = nameid1.user_name;
                //    detail1.id = nameid1.userid;

                //    bill.bill_updated_by = detail1;

                //var x = _Context.users.Where(c => c.paid_by_id.Any(y => y.billId == bills[j].billid)).ToList();
                //var pay = new List<BillDetail>();
                //for (int i=0;i<x.Count;i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == x[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.payers.SingleOrDefault(c => c.billId == bills[j].billid && c.paid_byId == x[i].userid);
                //    billDetail.amount = amtid.amount_paid;
                //    pay.Add(billDetail);
                //}
                //bill.payers = pay;

                //var p = _Context.users.Where(c => c.shared_with.Any(y => y.billId == bills[j].billid)).ToList();
                //var share = new List<BillDetail>();
                //for (int i = 0; i < p.Count; i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == p[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.sharedWiths.SingleOrDefault(c =>
                //    c.billId == bills[j].billid && c.shared_withId == p[i].userid);
                //    billDetail.amount = amtid.owes_amount;
                //    share.Add(billDetail);
                //}
                //bill.sharedwiths= share;

                //bill.bill_date = bills[j].bill_date;
                //    bill.bill_updated_at = bills[j].bill_updated_at;
                //    bill.description = bills[j].description;
                //    bill.total_amount = bills[j].total_amount;
                    billlist.Add(bill);
            }
            return billlist;
        }

        public async Task<List<BillGetResponse>> GetGroupBillsAsync(int groupid)
        {
            var billlist = new List<BillGetResponse>();
            var bills= _Context.bills.Where(c=>c.groupId==groupid).ToList();
            for (var j = 0; j < bills.Count; j++)
            {
                BillGetResponse bill = new BillGetResponse();
                bill = await GetBillAsync(bills[j].billid);
                //bill.billid = bills[j].billid;
                //bill.bill_created_at = bills[j].bill_created_at;

                //var detail = new Detail();
                //var nameid = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_created_byId);
                //detail.name = nameid.user_name;
                //detail.id = nameid.userid;

                //bill.bill_created_by = detail;

                //var detail1 = new Detail();
                //var nameid1 = _Context.users.SingleOrDefault(c => c.userid == bills[j].bill_updated_byId);
                //detail1.name = nameid1.user_name;
                //detail1.id = nameid1.userid;

                //bill.bill_updated_by = detail1;

                //var nameid2 = _Context.groups.SingleOrDefault(c => c.groupid == bills[j].groupId);
                //var detail2 = new Detail();
                //detail2.id = nameid2.groupid;
                //detail2.name = nameid2.group_name;
                //bill.group = detail2;

                //bill.bill_date = bills[j].bill_date;
                //bill.bill_updated_at = bills[j].bill_updated_at;
                //bill.description = bills[j].description;
                //bill.total_amount = bills[j].total_amount;
                //billlist.Add(bill);

                //var x = _Context.users.Where(c => c.paid_by_id.Any(y => y.billId == bills[j].billid)).ToList();
                //var pay = new List<BillDetail>();
                //for (int i = 0; i < x.Count; i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == x[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.payers.SingleOrDefault(c => c.billId == bills[j].billid && c.paid_byId == x[i].userid);
                //    billDetail.amount = amtid.amount_paid;
                //    pay.Add(billDetail);
                //}
                //bill.payers = pay;

                //var p = _Context.users.Where(c => c.shared_with.Any(y => y.billId == bills[j].billid)).ToList();
                //var share = new List<BillDetail>();
                //for (int i = 0; i < p.Count; i++)
                //{
                //    BillDetail billDetail = new BillDetail();
                //    var y = _Context.users.SingleOrDefault(c => c.userid == p[i].userid);
                //    billDetail.id = y.userid;
                //    billDetail.name = y.user_name;
                //    var amtid = _Context.sharedWiths.SingleOrDefault(c =>
                //    c.billId == bills[j].billid && c.shared_withId == p[i].userid);
                //    billDetail.amount = amtid.owes_amount;
                //    share.Add(billDetail);
                //}
                //bill.sharedwiths = share;
                billlist.Add(bill);
            }
            return billlist;
        }

        public async Task<bool> UpdateBillAsync(BillResponse billResponse)
        {
            Bill bill = new Bill();
            if (billResponse.groupId != 0)
            {
                 bill.groupId = billResponse.groupId;
            }
            bill.billid = billResponse.billid;
            bill.bill_created_at = billResponse.bill_created_at;
            bill.bill_created_byId = billResponse.bill_created_byId;
            bill.bill_date = billResponse.bill_date;
            bill.bill_updated_at = billResponse.bill_updated_at;
            bill.description = billResponse.description;
            bill.total_amount = billResponse.total_amount;
            bill.bill_updated_byId = billResponse.bill_updated_byId;

            _Context.bills.Attach(bill);
            _Context.Entry(bill).State = EntityState.Modified;

            int payer_count = (from n in _Context.payers
                               where n.billId== billResponse.billid
                               select n).Count();
            for(int i=0;i <payer_count;i++)
            {
                var pay = await _Context.payers
                         .FirstOrDefaultAsync(c => c.billId == billResponse.billid);
                _Context.payers.Remove(pay);
                await _Context.SaveChangesAsync();
            }           
            foreach (var x in billResponse.payers)
            {
                Payer payer = new Payer();
                payer.billId = bill.billid;
                payer.paid_byId = x.paid_byId;
                payer.amount_paid = x.amount_paid;
                _Context.payers.Add(payer);
            }

            int shared_count = (from n in _Context.sharedWiths
                                where n.billId == billResponse.billid
                                select n).Count();

            for (int i = 0; i < shared_count; i++)
            {
                var pay = await _Context.sharedWiths
                         .FirstOrDefaultAsync(c => c.billId == billResponse.billid);
                _Context.sharedWiths.Remove(pay);
                await _Context.SaveChangesAsync();
            }
            foreach (var x in billResponse.sharedwiths)
            {
                SharedWith sharedWith = new SharedWith();
                sharedWith.billId = bill.billid;
                sharedWith.shared_withId = x.shared_withId;
                sharedWith.owes_amount = x.owes_amount;
                _Context.sharedWiths.Add(sharedWith);
            }
            try
            {
                return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
                _Logger.LogError($"Error in {nameof(UpdateBillAsync)}: " + exp.Message);
            }
            return false;
        }
    }
}
