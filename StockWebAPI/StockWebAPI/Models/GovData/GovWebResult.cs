using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.GovData
{
    public class GovWebResult
    {
        public string stat { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public List<string> fields { get; set; }
        public List<List<object>> data { get; set; }
        public List<string> notes { get; set; }
    }
    public class OTCWebResult
    {
        public string stkNo { get; set; }
        public string stkName { get; set; }
        public string reportDate { get; set; }
        public int iTotalRecords { get; set; }
        public List<List<string>> aaData { get; set; }
        public int stkDivYear1 { get; set; }
        public int stkDivYear2 { get; set; }
        public int stkDivYear3 { get; set; }
        public double stkDivVal1 { get; set; }
        public string stkDivVal2 { get; set; }
        public string stkDivVal3 { get; set; }
    }

}
