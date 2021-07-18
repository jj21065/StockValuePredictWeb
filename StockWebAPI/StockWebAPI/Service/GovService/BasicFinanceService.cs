using Newtonsoft.Json;
using StockWebAPI.Common;
using StockWebAPI.Models;
using StockWebAPI.Models.GovData;
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
        SqlStockFinanceData sqlControl = new SqlStockFinanceData();
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

            MSSQLModule sqlModule = new MSSQLModule();
            string error  = sqlModule.SqlCommand("");
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

        public List<ValuePredictModel> GetValuePredictData()
        {
            List<ValuePredictModel> valuePredictList = new List<ValuePredictModel>();


            return valuePredictList;
        }

        public List<float> GetStockMonthRevenue(Dictionary<int,int> yearMonth,int stockId)
        {
            List<float> rs = sqlControl.GetStockMonthlyRevenue(yearMonth, stockId);
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
    }
}
