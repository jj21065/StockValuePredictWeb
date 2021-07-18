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
    [Route("api/[controller]")]
    [ApiController]
   
    public class StockApiController : ControllerBase
    {
        BasicFinanceService Service = new BasicFinanceService();
        /// <summary>
        /// Get Monthly profit report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMonthlyProfit")]
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
        [HttpGet]
        [Route("GetProfitDetail")]
        public async Task<ActionResult<List<ProfitDetailModel>>> GetProfitDetail()
        {
            
            List<ProfitDetailModel> resultList = new List<ProfitDetailModel>();

            resultList = Service.GetFinanceData();

            return resultList;
        }

        
    }
}

