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
    public class TransactionController : Controller
    {
        TransactionData _transactionData;
        ILogger _Logger;

        public TransactionController(TransactionData transactionData, ILoggerFactory loggerFactory)
        {
            _transactionData = transactionData;
            _Logger = loggerFactory.CreateLogger(nameof(BillController));
        }

        [Route("api/settleup")]
        [HttpPost]
        [ProducesResponseType(typeof(CommonResponse), 201)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> settleup([FromBody]TransactionResponse transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }
            try
            {
                var status = await _transactionData.SettleUpAsync(transaction);
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

        [Route("api/frd/expense/{userid}/{friendid}")]
        [HttpGet("{userid}/{friendid}")]
        [ProducesResponseType(typeof(List<TransactionGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> FriendExpense(int userid, int friendid)
        {
            try
            {
                var exp = await _transactionData.GetAllAmongFriend(userid, friendid);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/all/expense/{userid}")]
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(List<TransactionGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Expense(int userid)
        {
            try
            {
                var exp = await _transactionData.GetAll(userid);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/group/expense/{groupid}")]
        [HttpGet("{groupid}")]
        [ProducesResponseType(typeof(List<TransactionGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> GroupExpense(int groupid)
        {
            try
            {
                var exp = await _transactionData.GetAllAmongGroup(groupid);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/settlements")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSettlementResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> settlements()
        {
            try
            {
                var exp = await _transactionData.getSettlements();
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/settlements/{userid}/{friendid}")]
        [HttpGet("{userid}/{friendid}")]
        [ProducesResponseType(typeof(List<GetSettlementResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> settlements(int userid,int friendid)
        {
            try
            {
                var exp = await _transactionData.getFriendSettlement(userid, friendid);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/GroupSettlements/{userid}/{groupid}")]
        [HttpGet("{userid}/{groupid}")]
        [ProducesResponseType(typeof(List<GetSettlementResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Groupsettlements(int userid, int groupid)
        {
            try
            {
                var exp = await _transactionData.getGroupSettlement(userid, groupid);
                return Ok(exp);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }
    }
}
