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
    public class BillController : Controller
    {
        BillData _billdata;
        ILogger _Logger;

        public BillController(BillData billdata, ILoggerFactory loggerFactory)
        {
            _billdata = billdata;
            _Logger = loggerFactory.CreateLogger(nameof(BillController));
        }

        [Route("api/Bill/getGroupBills/{groupid}")]
        [HttpGet("{groupid}")]
        [ProducesResponseType(typeof(List<BillGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> GroupBills(int groupid)
        {
            try
            {
                var bills = await _billdata.GetGroupBillsAsync(groupid);
                return Ok(bills);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Bill/allbills/{userid}")]
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(List<BillGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AllUserBills(int userid)
        {
            try
            {
                var bills = await _billdata.GetAllBillsAsync(userid);
                return Ok(bills);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Bill/getFriendBills/{userid}/{friendid}")]
        [HttpGet("{userid}/{friendid}")]
        [ProducesResponseType(typeof(List<BillGetResponse>), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> FriendBills(int userid,int friendid)
        {
            try
            {
                var bills = await _billdata.GetFriendBillsAsync(userid,friendid);
                return Ok(bills);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Bill/getBill/{billid}")]
        [HttpGet("{billid}", Name = "GetBillRoute")]
        [ProducesResponseType(typeof(Bill), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> Bill(int billid)
        {
            try
            {
                var bill = await _billdata.GetBillAsync(billid);
                return Ok(bill);
            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Bill/addBill")]
        [HttpPost]
        [ProducesResponseType(typeof(CommonResponse), 201)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> AddBill([FromBody]BillResponse bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }

            try
            {
                var newBill = await _billdata.AddNewBillAsync(bill);
                if (newBill == null)
                {
                    return BadRequest(new CommonResponse { Status = false });
                }
                return Ok(new CommonResponse { Status = true, id=newBill.billid });

            }
            catch (Exception exp)
            {
                _Logger.LogError(exp.Message);
                return BadRequest(new CommonResponse { Status = false });
            }
        }

        [Route("api/Bill/deleteBill/{id}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> DeleteBill(int id)
        {
            try
            {
                var status = await _billdata.DeleteBillAsync(id);
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

        [Route("api/Bill/updateBill/{id}")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommonResponse), 200)]
        [ProducesResponseType(typeof(CommonResponse), 400)]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody]BillResponse bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new CommonResponse { Status = false });
            }

            try
            {
                var status = await _billdata.UpdateBillAsync(bill);
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
