using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.ResponseModel
{
    public class BillGetResponse
    {
        public int billid { get; set; }
        public string description { get; set; }
        public double total_amount { get; set; }
        public Detail bill_created_by { get; set; }
        public Detail bill_updated_by { get; set; }
        public Detail group { get; set; }
        public DateTime bill_date { get; set; }
        public DateTime bill_created_at { get; set; }
        public DateTime bill_updated_at { get; set; }
        public List<BillDetail> payers { get; set; }
        public List<BillDetail> sharedwiths { get; set; }
    }
    public class BillDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
    }
}
