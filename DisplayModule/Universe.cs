using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaewooBot_v2
{
    public partial class Universe : Form
    {
        Logs logs = new Logs();

        public Universe()
        {
            InitializeComponent();
        }


        public void DisplayTargetStocks(string type, 
                                        string stockCode, 
                                        string stockName, 
                                        string price, 
                                        string change, 
                                        string tickSpeed,
                                        string highPrice,
                                        string tickAvg,
                                        string beforeAvg,
                                        string totalVolume)
        {
            price = price.Replace("-", "");
            price = price.Replace("+", "");
            highPrice = price.Replace("-", "");

            if (type == "Insert")
            {
                int AddCnt = TargetStocks.Rows.Count;
                // StockCode, StockKrName, Price, TickSpeed, PnL

                TargetStocks.Rows.Add(stockCode, stockName, price, change, tickSpeed, highPrice, tickAvg, beforeAvg, totalVolume);
                                                      
                logs.write_sys_log(stockCode + "종목이 추가 되었습니다.", 0);
            }

            else if (type == "Update")
            {
                int UpdateCnt = TargetStocks.Rows.Count;
                for (int i = 0; i < UpdateCnt - 1; i++)
                {
                    if (TargetStocks["StockCode", i].Value.ToString() == stockCode)
                    {
                        TargetStocks.Rows[i].Cells[2].Value = String.Format("{0:0,0}", double.Parse(price));
                        TargetStocks.Rows[i].Cells[3].Value = String.Format("{0:0,0}", double.Parse(change));
                        TargetStocks.Rows[i].Cells[4].Value = String.Format("{0:0,0}", double.Parse(tickSpeed));
                        TargetStocks.Rows[i].Cells[5].Value = String.Format("{0:0,0}", double.Parse(highPrice));
                        TargetStocks.Rows[i].Cells[6].Value = String.Format("{0:0,0}", double.Parse(tickAvg));
                        TargetStocks.Rows[i].Cells[7].Value = String.Format("{0:0,0}", double.Parse(beforeAvg)); 
                        TargetStocks.Rows[i].Cells[8].Value = String.Format("{0:0,0}", double.Parse(totalVolume));
                        break;
                    }
                }
            }
        }

        public void DeleteTargetStocks(string StockCode)
        {
            try
            {
                int DelCnt = TargetStocks.Rows.Count;
                for (int i = 0; i < DelCnt; i++)
                {
                    try
                    {
                        if (TargetStocks["StockCode", i].Value.ToString() == StockCode && TargetStocks != null)
                        {
                            // 해당 데이터 삭제
                            TargetStocks.Rows.Remove(TargetStocks.Rows[i]);

                            logs.write_sys_log(StockCode + "종목이 삭제 되었습니다.", 0);
                            break;
                        }
                    }
                    catch (Exception err)
                    {
                        logs.write_sys_log(err.ToString(), 0);
                    }
                }
            }

            catch (Exception err)
            {
                logs.write_sys_log(err.ToString(), 0);
            }

        }
    }
}
