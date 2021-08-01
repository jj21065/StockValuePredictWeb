using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.StockApiOutput
{
    public class StockInfoModel
    {
        public int StockId { get; set; }
        public string StockName { get; set; }

        public string StockType { get; set; }
    }
    public enum CompanyType
    {
        SII,// 上市公司
        OTC, // 上櫃公司
    }
}
