using StockWebAPI.Common.SqlModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class SqlStockFinanceData
    {
        
        public List<float> GetStockMonthlyRevenue(Dictionary<int,int> yearMonths,int stockId)
        {
            List<float> monthR = new List<float>();
            SqlControl sql = new SqlControl();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "JAKE-NB",
                InitialCatalog = "StockDB",
                UserId = "Stock_Develop0",
                Password = "Stock_Develop0"
            };

            string conditionString = $" Where CompanyId ='{stockId}' and (";
            foreach (var ym in yearMonths)
            {
                conditionString = conditionString + $" (Year = '{ym.Key}' and Month = '{ym.Value}') or";
            }
            conditionString = conditionString.Remove(conditionString.Length - 3) + ")";
            try
            {
                string tblName = "dbo.MonthRevenueTbl";

                string sqlString = "Select Revenue From " + tblName + conditionString;
                DataTable tb = sql.SqlSelect(sqlString, para);
                foreach (DataRow row in tb.Rows)
                {
                    try
                    {
                        float r = Convert.ToSingle(row["Revenue"]);
                        monthR.Add(r);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(conditionString);
            }

            return monthR;
        
        }
    }
}
