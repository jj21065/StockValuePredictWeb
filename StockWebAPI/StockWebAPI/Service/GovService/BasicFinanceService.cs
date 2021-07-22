using Newtonsoft.Json;
using StockWebAPI.Common;
using StockWebAPI.Models;
using StockWebAPI.Models.GovData;
using StockWebAPI.Models.SqlModels;
using StockWebAPI.Models.StockApiInput;
using StockWebAPI.Models.StockApiOutput;
using StockWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockWebAPI.Service
{
    public class BasicFinanceService
    {
        SqlStockFinanceRepo repo = new SqlStockFinanceRepo();
        public List<MonthlyProfitModel> GetMonthlyProfitData()
        {
            List<MonthlyProfitModel> incomeList = new List<MonthlyProfitModel>();
            string targetUrl = "https://openapi.twse.com.tw/v1/opendata/t187ap05_L";
            string parame = "3552";
            byte[] postData = Encoding.UTF8.GetBytes(parame);

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
           // request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30000;
           // request.ContentLength = postData.Length;
            // 寫入 Post Body Message 資料流
            //using (Stream st = request.GetRequestStream())
            //{
            //    st.Write(postData, 0, postData.Length);
            //}

            string result = "";
            // 取得回應資料
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
           
                }
            }
            
            if(result.IndexOf('[') != -1)
            {
                result = result.Replace('[', ' ');
                result = result.Replace(']', ' ');
                string[] resultList = result.Split('\n');
                if(resultList != null)
                {
                    for(int i = 0;i<resultList.Length;i++)
                    {
                        if (resultList[i] != string.Empty)
                        {
                            try
                            {
                                MonthlyProfitModel incomeModel = JsonConvert.DeserializeObject<MonthlyProfitModel>(resultList[i].Remove(resultList[i].Length-1));
                                if (incomeModel != null)
                                {
                                    incomeList.Add(incomeModel);
                                }
                            }catch(Exception ex)
                            {

                            }
                        }
                    }
                }
            }

            
            return incomeList;
         
        }

        public List<ProfitDetailModel> GetFinanceData()
        {

            List<ProfitDetailModel> detailList = new List<ProfitDetailModel>();
            string targetUrl = "https://www.tpex.org.tw/openapi/v1/mopsfin_t187ap06_O_ci";
            string parame = "";
            byte[] postData = Encoding.UTF8.GetBytes(parame);

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30000;
            //request.ContentLength = postData.Length;
            // 寫入 Post Body Message 資料流
            //using (Stream st = request.GetRequestStream())
            //{
            //    st.Write(postData, 0, postData.Length);
            //}

            string result = "";
            // 取得回應資料
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();

                }
            }

            if (result.IndexOf('[') != -1)
            {
                result = result.Replace('[', ' ');
                result = result.Replace(']', ' ');
                string[] resultList = result.Split('\n');
                if (resultList != null)
                {
                    for (int i = 0; i < resultList.Length; i++)
                    {
                        if (resultList[i] != string.Empty)
                        {
                            try
                            {
                                ProfitDetailModel incomeModel = JsonConvert.DeserializeObject<ProfitDetailModel>(resultList[i].Remove(resultList[i].Length - 1));
                                if (incomeModel != null)
                                {
                                    detailList.Add(incomeModel);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return detailList;
        }

        public List<float> GetStockMonthRevenue(List<KeyValuePair<int, int>> yearMonth,int stockId)
        {
            List<float> rs = repo.GetStockMonthlyRevenue(yearMonth, stockId);
            return rs;
        }
        public string GetMonthlyDetail()
        {
        

            List<ProfitDetailModel> detailList = new List<ProfitDetailModel>();
            string targetUrl = "https://mops.twse.com.tw/mops/web/ajax_t163sb04";
            string parame = "{\"encodeURIComponent\" = \"1 \" ,\"step \": \"1 \",\"firstin \": \"1 \",\"off \": \"1 \",\"isQuery \": \"Y \",\"TYPEK \": \"sii\",\"year\": \"107\",\"season\": \"02\"}";
            byte[] postData = Encoding.UTF8.GetBytes(parame);

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 30000;
            request.ContentLength = postData.Length;
            // 寫入 Post Body Message 資料流
            using (Stream st = request.GetRequestStream())
            {
                st.Write(postData, 0, postData.Length);
            }

            string result = "";
            // 取得回應資料
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();

                }
            }

          
            return result;
        }

        public StockValuePredictModel GetPredictStockValue(StockValuePredictParaModel paraModel)
        {
            try
            {
                StockValuePredictModel responseModel = new StockValuePredictModel();
                
                StockApiParaModel inputModel = new StockApiParaModel()
                {
                    StockId = paraModel.CompanyId,
                    Year = paraModel.year,
                    Month = paraModel.Month,
                    Season = paraModel.Season
                };
                //repo.GetStockSeasonDetail(inputModel);
                List<KeyValuePair<int, int>> yearMonths = new List<KeyValuePair<int, int>> ();
                yearMonths.Add(new KeyValuePair<int,int>(paraModel.year,paraModel.Month));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month-1>0)?paraModel.year:paraModel.year-1), ((paraModel.Month-1)<0)?12:paraModel.Month-1));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month - 2 >0) ? paraModel.year: paraModel.year - 2), ((paraModel.Month - 2) < 0) ? 11 : paraModel.Month - 2));

                List<float> revenueMonths = repo.GetStockMonthlyRevenue(yearMonths, paraModel.CompanyId);

                StockFinanceSeasonDetailModel detailModel = repo.GetStockSeasonDetail(inputModel);

                if (revenueMonths.Count > 0)
                {
                    float seasonRevenue = revenueMonths.Sum();
                    double marginProfit = (detailModel.Revenue - detailModel.Cost) / detailModel.Revenue;
                    double predictMargin = seasonRevenue * marginProfit;
                    double predictSeasonProfitAfterTax = (predictMargin - detailModel.OperatingFee + detailModel.NonOperatingProfit) * 0.8;

                    //EPS 
                    double predictEPS = predictSeasonProfitAfterTax * detailModel.EPS / detailModel.TotalProfitAfterTax * 4;
                    double predictValue = paraModel.PERatio * predictEPS;

                    responseModel.PredictStockValue = predictValue;
                    responseModel.Year = inputModel.Year;
                    responseModel.CompanyId = detailModel.CompanyId;
                    responseModel.CompanyName = detailModel.CompanyName;

                   // responseModel.CompanyName = "test";
                    return responseModel;
                }
                return null;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public StockValuePredictModel GetHistoryPeRatio(StockValuePredictParaModel paraModel)
        {
            try
            {
                StockValuePredictModel responseModel = new StockValuePredictModel();

                StockApiParaModel inputModel = new StockApiParaModel()
                {
                    StockId = paraModel.CompanyId,
                    Year = paraModel.year,
                    Month = paraModel.Month,
                    Season = paraModel.Season
                };
                //repo.GetStockSeasonDetail(inputModel);
                List<KeyValuePair<int, int>> yearMonths = new List<KeyValuePair<int, int>>();
                yearMonths.Add(new KeyValuePair<int, int>(paraModel.year, paraModel.Month));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month - 1 > 0) ? paraModel.year : paraModel.year - 1), ((paraModel.Month - 1) < 0) ? 12 : paraModel.Month - 1));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month - 2 > 0) ? paraModel.year : paraModel.year - 2), ((paraModel.Month - 2) < 0) ? 11 : paraModel.Month - 2));

                List<float> revenueMonths = repo.GetStockMonthlyRevenue(yearMonths, paraModel.CompanyId);

                StockFinanceSeasonDetailModel detailModel = repo.GetStockSeasonDetail(inputModel);

                if (revenueMonths.Count > 0)
                {
                    float seasonRevenue = revenueMonths.Sum();
                    double marginProfit = (detailModel.Revenue - detailModel.Cost) / detailModel.Revenue;
                    double predictMargin = seasonRevenue * marginProfit;
                    double predictSeasonProfitAfterTax = (predictMargin - detailModel.OperatingFee + detailModel.NonOperatingProfit) * 0.8;

                    //EPS 
                    double predictEPS = predictSeasonProfitAfterTax * detailModel.EPS / detailModel.TotalProfitAfterTax * 4;
                    double predictValue = paraModel.PERatio * predictEPS;

                    responseModel.PredictStockValue = predictValue;
                    responseModel.Year = inputModel.Year;
                    responseModel.CompanyId = detailModel.CompanyId;
                    responseModel.CompanyName = detailModel.CompanyName;

                    // responseModel.CompanyName = "test";
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
