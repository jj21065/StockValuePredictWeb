using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Models.SqlModels
{
    public class SqlDailyTop20TradingModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public float TransactionShares { get; set; }
        public float TransactionAmount { get; set; }
        public float OpeningPrice { get; set; }
        public float HighestPrice { get; set; }
        public float LowestPrice { get; set; }
        public float ClosingPrice { get; set; }
        public string UpsDowns { get; set; }
        public float PriceDifference { get; set; }
        public float LastRevealBuyPrice { get; set; }
        public float LastRevealSealPrice { get; set; }
        public int Rank { get; set; }

        public string tDate { get; set; }
    }
}
