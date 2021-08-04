using System;

namespace StockWebAPI.Models.SqlModels
{
    public class SqlFinanceSeasonDetailModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int Year { get; set; }
        public int Season { get; set; }

        /// <summary>
        /// 近一季營收
        /// </summary>
        public double Revenue { get; set; }
        /// <summary>
        /// 成本支出
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// 進一季營業費用
        /// </summary>
        public double OperatingFee { get; set; }
        /// <summary>
        /// 業外收支
        /// </summary>
        public double NonOperatingProfit { get; set; }

        /// <summary>
        /// 繼續營業單位損益
        /// </summary>
        public double TotalProfitAfterTax { get; set; }
        /// <EPS>
        /// 近一季營業費用
        /// </summary>
        public double EPS { get; set; }


       

    }
}
