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
    public partial class Position : Form
    { 
        public Position()
        {
            InitializeComponent();
        }


        public void DisplayAccount(List<string> accountList)
        {
            if (TodayDataGrid.InvokeRequired)
            {
                TodayDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    TodayDataGrid.Rows[0].Cells[0].Value = accountList[0];
                    TodayDataGrid.Rows[0].Cells[1].Value = accountList[1];
                    TodayDataGrid.Rows[0].Cells[2].Value = accountList[2];
                }));
            }
            else
            {
                TodayDataGrid.Rows[0].Cells[0].Value = accountList[0];
                TodayDataGrid.Rows[0].Cells[1].Value = accountList[1];
                TodayDataGrid.Rows[0].Cells[2].Value = accountList[2];
            }
        }

        public void DisplayPosition(Dictionary<string, PositionState> positionDict)
        {


            List<string> codes = positionDict.Keys.ToList();
            var dataGridCnt = PositionDataGrid.Rows.Count;
            var positionDictCnt = positionDict.Count;

            if (PositionDataGrid.InvokeRequired)
            {
             
                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {

                    for (int positionCnt = 0; positionCnt < positionDictCnt; positionCnt++)
                    {
                        var shortCode = codes[positionCnt];
                        var krName = positionDict[shortCode].position_KrName;
                        var balance = positionDict[shortCode].position_BalanceQty;
                        var buyPrice = positionDict[shortCode].position_BuyPrice;
                        var curPrice = positionDict[shortCode].position_CurPrice;
                        var change = positionDict[shortCode].position_Change;
                        var tradingPnL = positionDict[shortCode].position_TradingPnL;

                        for(int gridCnt = 0; gridCnt < dataGridCnt; gridCnt++)
                        {
                            if (PositionDataGrid["Position_StockCode", gridCnt].Value.ToString() == codes[positionCnt].ToString())
                            {
                                PositionDataGrid["Position_StockCode", gridCnt].Value = shortCode;
                                PositionDataGrid["Position_KrName", gridCnt].Value = krName;
                                PositionDataGrid["BalanceQty", gridCnt].Value = balance;
                                PositionDataGrid["BuyPrice", gridCnt].Value = buyPrice;
                                PositionDataGrid["CurPrice", gridCnt].Value = curPrice;
                                PositionDataGrid["Change", gridCnt].Value = change;
                                PositionDataGrid["TradingPnL", gridCnt].Value = tradingPnL;
                                break;
                            }
                            else
                                PositionDataGrid.Rows.Add(shortCode, krName, balance, buyPrice, curPrice, change, tradingPnL);
                        }
                    }
                }));
            }
            else
            {
                for (int positionCnt = 0; positionCnt < positionDictCnt; positionCnt++)
                {
                    var shortCode = codes[positionCnt];
                    var krName = positionDict[shortCode].position_KrName;
                    var balance = positionDict[shortCode].position_BalanceQty;
                    var buyPrice = positionDict[shortCode].position_BuyPrice;
                    var curPrice = positionDict[shortCode].position_CurPrice;
                    var change = positionDict[shortCode].position_Change;
                    var tradingPnL = positionDict[shortCode].position_TradingPnL;

                    for (int gridCnt = 0; gridCnt < dataGridCnt; gridCnt++)
                    {
                        if (PositionDataGrid["Position_StockCode", gridCnt].Value.ToString() == codes[positionCnt].ToString())
                        {
                            PositionDataGrid["Position_StockCode", gridCnt].Value = shortCode;
                            PositionDataGrid["Position_KrName", gridCnt].Value = krName;
                            PositionDataGrid["BalanceQty", gridCnt].Value = balance;
                            PositionDataGrid["BuyPrice", gridCnt].Value = buyPrice;
                            PositionDataGrid["CurPrice", gridCnt].Value = curPrice;
                            PositionDataGrid["Change", gridCnt].Value = change;
                            PositionDataGrid["TradingPnL", gridCnt].Value = tradingPnL;
                            break;
                        }
                        else
                            PositionDataGrid.Rows.Add(shortCode, krName, balance, buyPrice, curPrice, change, tradingPnL);
                    }
                }
            }
        }
    }
}
