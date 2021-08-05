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
    public partial class Blotter : Form
    {
        Utils utils = new Utils();
        Position position = new Position();
        BotParams botParams = new BotParams();

        public Blotter()
        {
            InitializeComponent();

            // Tr code 검색
            this.BLT_API.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(this.OnReceiveChejanData);
         

        }

        private void OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            /*
            sGubun 0 : 체결


            9001 : 종목코드  ShortCode
            913 : 주문상태   
            302 : 종목명     KrName
            900 : 주문수량   OrdQty
            911 : 체결수량   FilledQty
            901 : 주문가격   OrdPrice
            910 : 체결가격   FilledPrice
            913 : 주문상태   OrdType ( new, amend, cancel )
            905 : 매매구분   Type    ( buy / sell )
            908 : 주문/체결시간
             */
            
            switch(e.sGubun)
            {
                case "0":
                    var OrderTime = BLT_API.GetChejanData(908).Trim().ToString();
                    var ShortCode = BLT_API.GetChejanData(9001).Trim().ToString();
                    var KrName = BLT_API.GetChejanData(302).Trim().ToString();
                    var OrderType = BLT_API.GetChejanData(913).Trim().ToString();
                    var Type = BLT_API.GetChejanData(905).Trim().ToString();
                    var OrderQty = BLT_API.GetChejanData(900).Trim().ToString();
                    var FilledQty = BLT_API.GetChejanData(911).Trim().ToString();
                    var OrderPrice = BLT_API.GetChejanData(901).Trim().ToString();
                    var FilledPrice = BLT_API.GetChejanData(910).Trim().ToString();

                    DisplayBLT(OrderTime, ShortCode, KrName, OrderType, Type, OrderQty, FilledQty, OrderPrice, FilledPrice);
                    break;

                case "1":

                    botParams.Deposit = double.Parse(BLT_API.GetChejanData(951).Trim().ToString());

                    var ShortCode1 = BLT_API.GetChejanData(9001).Trim().ToString();
                    var KrName1 = BLT_API.GetChejanData(302).Trim().ToString();
                    var BalanceQty = BLT_API.GetChejanData(930).Trim().ToString();
                    var BuyPrice = BLT_API.GetChejanData(931).Trim().ToString();
                    var CurPrice = BLT_API.GetChejanData(10).Trim().ToString();
                    var Change = BLT_API.GetChejanData(8019).Trim().ToString();
                    var TradingPnL = (double.Parse(CurPrice) - double.Parse(BuyPrice)) * double.Parse(BalanceQty);

                    var todayPnL = BLT_API.GetChejanData(990).Trim().ToString();
                    var todayChange = BLT_API.GetChejanData(991).Trim().ToString();

                    position.DisplayAccount(todayPnL, todayChange, botParams.Deposit.ToString());

                    position.DisplayPosition(ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString());
                    break;
                    
            }
        }

        private void DisplayBLT(string OrderTime, string ShortCode, string KrName, string OrderType, string Type, string OrderQty, string FilledQty, string OrderPrice, string FilledPrice)
        {
            if (OrderType == "접수")
            {
                if (BltDataGrid.InvokeRequired)
                {
                    BltDataGrid.Invoke(new MethodInvoker(delegate ()
                    {
                        BltDataGrid.Rows.Add(OrderTime, ShortCode, KrName, OrderType, Type, OrderQty, FilledQty, OrderPrice, FilledPrice);
                    }));
                }
                else
                {
                    BltDataGrid.Rows.Add(OrderTime, ShortCode, KrName, OrderType, Type, OrderQty, FilledQty, OrderPrice, FilledPrice);
                }
            }

            else
            {

            }
        }

        public void SendOrder()
        {
            botParams.RqName = "주식주문";
            var scr_no = utils.get_scr_no();
            var ShortCode = botParams.SignalStockCode;
            var curPrice = botParams.SignalPrice;
            int ordQty = Int32.Parse(Math.Truncate(1000000.0 / double.Parse(curPrice)).ToString());
            var ordPrice = 0;
            var hogaGb = "03";

            BLT_API.SendOrder(botParams.RqName, scr_no, botParams.AccountNumber, 1, ShortCode, ordQty, ordPrice ,hogaGb, "");

            //main.write_sys_log($"Order ShrotCode : {ShortCode}  lots : {ordQty.ToString()} Order Price : 시장가", 0);

        }
    }
}
