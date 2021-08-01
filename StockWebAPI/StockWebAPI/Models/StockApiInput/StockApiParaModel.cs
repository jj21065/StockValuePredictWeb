using StockWebAPI.Models.StockApiOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.StockApiInput
{
    public class StockApiParaModel
    {
        public int Year{get;set;}
        public int Month { get; set; }

        public int Season { get; set; }

        public StockInfoModel StockInfo { get; set; } = new StockInfoModel();

    }
}
