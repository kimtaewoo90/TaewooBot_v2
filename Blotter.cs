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

        public TelegramClass telegram = new TelegramClass();
        public Utils utils = new Utils();

        public Blotter()
        {
            InitializeComponent();
            this.BLT_API.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(this.OnReceiveChejanData);
        }


        private void OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            /*
            sGubun 0 : 접수와 체결
            sGubun 1 : 국내주식 잔고변경

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

            switch (e.sGubun)
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

                    BotParams.BltList.Add(new List<string> { OrderTime, ShortCode, KrName, OrderType, Type, OrderQty, FilledQty, OrderPrice, FilledPrice });

                    // 매수 매도 후 잔고 갱신
                    BLT_GetAccountInformation();

                    break;

                // 국내주식 잔고변경
                case "1":
                    BotParams.Deposit = double.Parse(BLT_API.GetChejanData(951).Trim().ToString());
                    var ShortCode1 = BLT_API.GetChejanData(9001).Trim().ToString();
                    var KrName1 = BLT_API.GetChejanData(302).Trim().ToString();
                    var BalanceQty = BLT_API.GetChejanData(930).Trim().ToString();
                    var BuyPrice = BLT_API.GetChejanData(931).Trim().ToString();
                    var CurPrice = BLT_API.GetChejanData(10).Trim().ToString();
                    var Change = BLT_API.GetChejanData(8019).Trim().ToString();
                    var TradingPnL = (double.Parse(CurPrice) - double.Parse(BuyPrice)) * double.Parse(BalanceQty);

                    BotParams.PositionList.Add(new List<string> { ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString() });

                    // TODO
                    var state = new PositionState(ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString());
                    BotParams.PositionDict[ShortCode1] = state;

                    //position.DisplayPosition(ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString());

                    var todayPnL = BLT_API.GetChejanData(990).Trim().ToString();
                    var todayChange = BLT_API.GetChejanData(991).Trim().ToString();

                    // BotParams 계좌정보 Dictionary Update
                    BotParams.Accnt_StockName[ShortCode1] = KrName1;
                    BotParams.Accnt_StockLots[ShortCode1] = BalanceQty;
                    BotParams.Accnt_StockPnL[ShortCode1] = Change;
                    BotParams.Accnt_StockPnL_Won[ShortCode1] = TradingPnL.ToString();

                    BotParams.AccountList.Add(new List<string> { KrName1, BalanceQty, Change, TradingPnL.ToString() });


                    /* 매도 시 Mode Class를 호출하여 각각의 Mode에서의 전략으로 매도 실행 */
                    /* ver2.0 에서는 여기에 바로 매도 로직 작성 */


                    // 매도 주문
                    if (BotParams.TickOneMinsList[ShortCode1].Average() < 0 
                        || double.Parse(Change) > 3.0 || double.Parse(Change) < -0.9 
                        || double.Parse(CurPrice) < double.Parse(BotParams.stockState[ShortCode1].states_highPrice) * 0.99)

                    {
                        telegram.SendTelegramMsg($"{KrName1}의 수익률 {Change.ToString()} % / TradingPnL : {TradingPnL.ToString()}");

                        // Order 주문은 다 여기서 처리.
                        // 시장가 매도 주문

                        BotParams.RqName = "주식주문";
                        var scr_no = utils.get_scr_no();
                        var ordPrice = 0;
                        var hogaGb = "03";

                        SendSellOrder(scr_no, ShortCode1, int.Parse(BalanceQty), ordPrice, hogaGb);

                        // 해당 종목 매도 시 다시 Tick Avg 계산.
                        BotParams.TickList.Remove(ShortCode1);
                    }

                    break;
            }
        }


        public void SendBuyOrder(string scr_no, string ShortCode, int ordQty, long ordPrice, string hogaGB)
        {

            telegram.SendTelegramMsg($"BuyOrder {ShortCode}/{ordQty}  / ShortCode length : {ShortCode.Length} ordQty length : {ordQty.ToString().Length}");

            try
            {
                // TODO : 매수잔량 취소 기능 추가
                var res = BLT_API.SendOrder("주식주문", scr_no, "8003542111", 1, ShortCode.Trim(), ordQty, 0, hogaGB, "");

                if (res == 0) telegram.SendTelegramMsg($"Success to Send BuyOrder {ShortCode}/{ordQty.ToString()}");
                else telegram.SendTelegramMsg($"[{ShortCode}] Failed to Send BuyOrder, res : {res.ToString()}");
            }
            catch (Exception e)
            {
                telegram.SendTelegramMsg($"Exception in Send BuyOrder errMsg : {e}");
            }

        }

        public void SendSellOrder(string scr_no, string ShortCode, int ordQty, int ordPrice, string hogaGB)
        {
            telegram.SendTelegramMsg($"SellOrder {ShortCode}/{ordQty}  / ShortCode length : {ShortCode.Length} ordQty length : {ordQty.ToString().Length}");
            try
            {
                var res = BLT_API.SendOrder("주식주문", scr_no, "8003542111", 3, ShortCode.Trim(), ordQty, 0, "03", "");

                if (res == 0) telegram.SendTelegramMsg($"Success to Send SellOrder {ShortCode}/{ordQty.ToString()}");
                else telegram.SendTelegramMsg($"[{ShortCode}] Failed to Send SellOrder, res : {res.ToString()}");
            }
            catch (Exception e)
            {
                telegram.SendTelegramMsg($"Exception in send SellOrder errMsg : {e.ToString()}");
            }
        }



    // TODO : 신규주문(접수) 일 때 말고 정정 및 취소 일때 Action 추가
    public void DisplayBLT(List<List<string>> bltData)
    {

            if (BltDataGrid.InvokeRequired)
            {
                BltDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    for (int i = 0; i < bltData.Count; i++)
                    {
                        BltDataGrid.Rows.Add(bltData[i][0], 
                                             bltData[i][1], 
                                             bltData[i][2], 
                                             bltData[i][3], 
                                             bltData[i][4], 
                                             bltData[i][5], 
                                             bltData[i][6], 
                                             bltData[i][7], 
                                             bltData[i][8]);
                    }
                }));
            }
            else
            {
                BltDataGrid.Rows.Add(bltData[0], bltData[1], bltData[2], bltData[3], bltData[4], bltData[5], bltData[6], bltData[7], bltData[8]);
            }
        }
        public void BLT_GetAccountInformation()
        {
            string scr_no = utils.get_scr_no();
            BLT_API.SetInputValue("계좌번호", BotParams.AccountNumber);
            BLT_API.SetInputValue("비밀번호", "");
            BLT_API.SetInputValue("상장폐지조회구분", "0");
            BLT_API.SetInputValue("비밀번호입력매체구분", "00");


            //BotParams.RqName = "계좌평가현황요청";
            BLT_API.CommRqData("계좌평가현황요청", "OPW00004", 0, scr_no);
        }
    }



}

