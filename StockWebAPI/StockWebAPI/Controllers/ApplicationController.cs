using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebAPI.Models.GovData;
using StockWebAPI.Service;

namespace StockWebAPI.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        BasicFinanceService Service = new BasicFinanceService();
        /// <summary>
        /// Get Monthly profit report
        /// </summary>
        /// <returns></returns>
        [HttpGet( Name = "MonthlyProfit")]
        public async Task<ActionResult<List<MonthlyProfitModel>>> GetMonthlyProfit()
        {
            
            List<MonthlyProfitModel> resultList = new List<MonthlyProfitModel>();

            resultList = Service.GetMonthlyProfitData();
            
            return resultList;
        }


        /// <summary>
        /// get total profit detail
        /// </summary>
        /// <returns></returns>
        [HttpPost( Name = "Detail")]
        public async Task<ActionResult<List<ProfitDetailModel>>> GetProfitDetail()
        {
            
            List<ProfitDetailModel> resultList = new List<ProfitDetailModel>();

            resultList = Service.GetFinanceData();

            return resultList;
        }
    }
}

