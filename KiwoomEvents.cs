using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace TaewooBot_v2
{
    public partial class Main
    {

        // Send Condition
        private void OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            //conditionList = new List<ConditionInfo>();

            string condtionNameList = API.GetConditionNameList();
            string[] conditionNameArray = condtionNameList.Split(';');

            for (int i = 0; i < conditionNameArray.Length; i++)
            {
                string[] ConditionInfo = conditionNameArray[i].Split('^');
                if (ConditionInfo.Length == 2)
                {
                    //conditionList.Add(new ConditionInfo()
                    //{
                   //     ConditionNum = int.Parse(ConditionInfo[0].Trim()),
                  //      ConditionNm = ConditionInfo[1].Trim()
                  //  });
                }
            }

            //API.SendCondition(utils.get_scr_no(), conditionList[0].ConditionNm, conditionList[0].ConditionNum, 1);


        }

        // Get Condition
        private void OnReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            if (e.strCodeList.Length > 0)
            {
                string stockCodeList = e.strCodeList.Remove(e.strCodeList.Length - 1);
                int stockCount = stockCodeList.Split(';').Length;

                if (stockCount <= 100)
                {
                    //g_rqname = "조건검색종목";
                    API.CommKwRqData(stockCodeList, 0, stockCount, 0, "조건검색종목", "9999");
                }
            }
            else if (e.strCodeList.Length == 0)
            {
                logs.write_sys_log("검색된 조건목록에 대한 종목이 없습니다.\n", 0);
            }
        }
        private void OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {

            //종목 편입
            if (e.strType.Equals("I"))
            {
                string stockName = API.GetMasterCodeName(e.sTrCode);
                logs.write_sys_log("편입종목 : " + "[" + stockName + " ].\n", 0);

                string scr_no = null;
                scr_no = "";
                scr_no = utils.get_scr_no();

                string msg = "편입종목 : " + stockName;

                logs.write_sys_log(msg, 0);

                bool DataGrid = false;
                for(int i=0; i < universe.TargetStocks.Rows.Count-1; i++)
                {
                    // 기존 TargetStocks Dict에 편입종목이 포함되어 있는지 확인.
                    if(universe.TargetStocks["StockCode", i].Value.ToString() == e.sTrCode)
                    {
                        DataGrid = true;
                        break;
                    }
                }

                // 기존 TargetStocks Dict에 지금 들어온 종목이 없을 때만 TargetStocks DataGridView 추가
                if (DataGrid == false)
                {
                    universe.DisplayTargetStocks("Insert", e.sTrCode.ToString(), stockName, "0", "0", "0", "","","","");
                }



                utils.delay(1000);


                // 추가된 종목만 따로 실시간 요청
                //ReqRealData(e.sTrCode, scr_no);


                // 주가 데이터 요청.
                BotParams.RqName = "";
                BotParams.RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", e.sTrCode);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(BotParams.RqName, "OPT10001", 0, scr_no);
            }

            //종목 이탈
            else if (e.strType.Equals("D"))
            {
                string stockName = API.GetMasterCodeName(e.sTrCode);
                logs.write_sys_log("이탈종목 : " + "[" + stockName + " ].\n", 0);

                //string scr_no = targetDict[e.sTrCode];
                //API.DisconnectRealData(scr_no);

                int i = 0;
                for (i = 0; i < BotParams.TargetCodes.Count; i++)
                {
                    if (BotParams.TargetCodes[i].Equals(e.sTrCode))
                    {
                        BotParams.TargetCodes.RemoveAt(i);
                        //break;
                    }
                }

                BotParams.TargetCodes.Remove(e.sTrCode);

                // Remove 이탈Stock in Dictionary
                utils.RemoveDict(e.sTrCode);

                logs.write_sys_log("이탈종목 : " + stockName, 0);

                //DeleteTargetStocks(e.sTrCode.ToString());
            }

            logs.write_sys_log(BotParams.TargetCodes.ToString(), 0);

        }

        // Get TR Data
        private void OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            try
            {
                if (BotParams.RqName.CompareTo(e.sRQName) != 0)
                {
                    logs.write_sys_log($"Should check the TRCode, e.sRQName is {e.sRQName}", 0);
                }
                else
                {
                    switch (BotParams.RqName)
                    {
                        // OPW00001
                        case "예수금상세현황요청":
                            logs.write_sys_log("예수금상세현황 요청", 0);
                            break;

                        // OPW00004
                        case "계좌평가현황요청":
                            logs.write_sys_log("계좌평가현황 요청", 0);
                            break;

                        case "호가조회":
                            logs.write_sys_log("호가조회 요청", 0);
                            break;

                        case "현재가조회":
                            logs.write_sys_log("현재가조회 요청", 0);
                            break;

                        case "주식시세":  // for 거래량 조회
                            logs.write_sys_log("주식시세 요청", 0);

                            break;

                        case "주식기본정보":  // for 주식기본정보
                            logs.write_sys_log("주식기본정보 요청", 0);
                            break;


                        default: break;
                    }
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
           

            if (e.sRQName == "현재가조회")
            {
                 API.CommGetData(e.sTrCode, "", e.sRQName, 0, "현재가");

            }

            if (e.sRQName == "주식기본정보")
            {
                string Code = API.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim().ToString();
                string KrName = API.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim().ToString();
                string Price = API.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim().ToString();

                universe.DisplayTargetStocks("Insert", Code, KrName, Price, "0", "0", "","","","");
                logs.write_sys_log($"[{BotParams.StockCnt}번째] {KrName}의 주식기본정보를 받아오는데에 성공하였습니다.", 0);

                // Update Dict
                try
                {
                    // Initialize BotParams parameters
                    List<double> initializeTickList = new List<double>() { 0.0};
                    List<double> initializeTickOneMinList = new List<double>() { 0.0 };
                    

                    BotParams.TickList[Code] = initializeTickList;
                    BotParams.TickOneMinsList[Code] = initializeTickOneMinList;
                    BotParams.BeforeAvg[Code] = BotParams.TickList[Code].Average();

                    var state = new StockState(Code, KrName, Price, "0", "0", "0", "0", BotParams.TickList[Code], BotParams.TickOneMinsList[Code], DateTime.Parse(BotParams.CurTime));
                    BotParams.stockState[Code] = state;

                }
                catch (Exception err)
                {
                    logs.write_sys_log(err.ToString(), 0);
                }

                logs.write_sys_log("주식기본정보 받기 성공", 0);
                
            }

            if (e.sRQName == "조건검색종목")
            {
                int count = API.GetRepeatCnt(e.sTrCode, e.sRQName); //요청의 반복 횟수를 요청합니다.
                logs.write_sys_log("조건검색종목 요청", 0);

                for (int i = 0; i < count; i++)
                {
                    string stockCode = API.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();
                    string stockName = API.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                    double currentPrice = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Replace("-", ""));
                    double upDownRate = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, i, "등락율"));
                    int netChange = int.Parse(API.GetCommData(e.sTrCode, e.sRQName, i, "전일대비"));
                    long volume = long.Parse(API.GetCommData(e.sTrCode, e.sRQName, i, "거래량"));                
                }
            }

            if (e.sRQName == "예수금상세현황요청")
            {
                BotParams.Deposit = Double.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim());
                logs.write_sys_log(BotParams.Deposit.ToString(), 0);
            }

            if (e.sRQName == "계좌평가현황요청")
            {
                BotParams.Deposit = Double.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim());
                BotParams.TotalPnL = Int32.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "당일손익율").Trim());


                setText_Control(AccountTextBox, BotParams.AccountNumber.ToString());
                setText_Control(DepositTextBox, BotParams.Deposit.ToString());
                setText_Control(TotalPnLTextBox, BotParams.TotalPnL.ToString());

                BotParams.AccountStockLots = API.GetRepeatCnt(e.sTrCode, e.sRQName);

                List<string> SentOrderList = new List<string>();

                for (int nIdx = 0; nIdx < BotParams.AccountStockLots; nIdx++)
                {
                    var code = API.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    code = code.Replace("A", "");
                    var krName = API.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목명").Trim().ToString();
                    var balance = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, nIdx, "보유수량").Trim());
                    var buyPrice = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, nIdx, "평균단가").Trim());
                    var curPrice = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim().Replace("-", ""));
                    var change = Math.Round((curPrice / buyPrice) - 1, 2);
                    var tradingPnL = double.Parse(API.GetCommData(e.sTrCode, e.sRQName, nIdx, "손익금액").Trim());

                    var positionState = new PositionState(code, krName, balance.ToString(), buyPrice.ToString(), curPrice.ToString(), change.ToString(), tradingPnL.ToString());

                    BotParams.Accnt_Position[code] = positionState;

                    position.DisplayPosition(code, 
                                             krName, 
                                             balance.ToString(), 
                                             buyPrice.ToString(), 
                                             curPrice.ToString(), 
                                             change.ToString(), 
                                             tradingPnL.ToString());

                }

                SentOrderList = new List<string>();
                BotParams.ArrangingPosition = false;

                if (BotParams.AccountStockLots == 0)
                {
                    logs.write_sys_log("보유종목이 없습니다.", 0);
                }
            }
        }

        private void OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            logs.write_sys_log($"ScrNo : {e.sScrNo} RQName : {e.sRQName} TrCode : {e.sTrCode} Msg : {e.sMsg}", 0);
            telegram.SendTelegramMsg($"ScrNo : {e.sScrNo} RQName : {e.sRQName} TrCode : {e.sTrCode} Msg : {e.sMsg}");
        }


        // Get Real Data
        private void OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            // A03
            if (e.sRealType == "주식체결")
            {
                string code = e.sRealKey;
                string krName = GetKrName(code);
                double price = Math.Abs(double.Parse(API.GetCommRealData(e.sRealType, 10).Trim()));  // current price;
                string contractLots = API.GetCommRealData(e.sRealType, 15).Trim().ToString(); // 체결량
                //double buy_price = get_hoga_unit_price((int)Price, Code, -2);
                double startPrice = Math.Abs(double.Parse(API.GetCommRealData(e.sRealType, 16).Trim()));
                double highPrice = Math.Abs(double.Parse(API.GetCommRealData(e.sRealType, 17).Trim()));
                double change = Math.Round((price / startPrice - 1) * 100, 2);
                double lowPrice = double.Parse(API.GetCommRealData(e.sRealType, 18).Trim());
                string TickTime = API.GetCommRealData(e.sRealType, 20).Trim().ToString();

                // Update stockState Dictionary
                var state = new StockState(code, 
                                           krName, 
                                           price.ToString(), 
                                           highPrice.ToString(),
                                           lowPrice.ToString(),
                                           change.ToString(), 
                                           contractLots, 
                                           BotParams.TickList[code], 
                                           BotParams.TickOneMinsList[code],
                                           BotParams.comparedTime);
                BotParams.stockState[code] = state;         

                // Save TickLog.txt
                File.AppendAllText(BotParams.TickPath + BotParams.TickLogFileName, $"[{BotParams.CurTime}] : {code} / {price.ToString()} / {change.ToString()} / {contractLots}" + Environment.NewLine, Encoding.Default);

                // Display the RTD data on TargetStocks DataGridView    TODO: Change -> startPrice to check
                universe.DisplayTargetStocks("Update", 
                                                code, 
                                                "", 
                                                price.ToString(), 
                                                change.ToString(), 
                                                BotParams.TickOneMinsList[code].Average().ToString(), 
                                                highPrice.ToString(), 
                                                BotParams.TickList[code].Average().ToString(), 
                                                BotParams.BeforeAvg[code].ToString(), 
                                                (BotParams.TickOneMinsList[code].Sum() * price).ToString());

                // Display Position in Real Time
                if (BotParams.Accnt_Position.ContainsKey(code))
                {
                    var shortCode = BotParams.Accnt_Position[code].position_ShortCode;
                    var balanceQty = BotParams.Accnt_Position[code].position_BalanceQty;
                    var buyPrice = BotParams.Accnt_Position[code].position_BuyPrice;
                    var change_in_position = (price / double.Parse(buyPrice) - 1) * 100;
                    var tradingPnL_in_position = (price - double.Parse(buyPrice)) * double.Parse(balanceQty); 
                    position.DisplayPosition(shortCode, krName, balanceQty, buyPrice, price.ToString(), change_in_position.ToString(), tradingPnL_in_position.ToString());
                }

                // Strategy1 
                strategy1.CalculateTickSpeed(code, contractLots, price, startPrice);
                
                // Buy Signals
                bool signalsStocks = state.MonitoringSignals_Strategy1();

                // 매수
                if (signalsStocks is true)
                {
                    // reset signal.
                    state.signal_1 = false;
                    state.signal_2 = false;
                    state.signal_3 = false;

                    // 매수 주문
                    if (BotParams.Deposit > 1000000 && !BotParams.Accnt_Position.ContainsKey(code) && !BotParams.PendingOrders.Contains(code))
                    {
                        logs.write_sys_log($"{code} Signal is true", 0);

                        telegram.SendTelegramMsg($"[{code}/{krName}] try to send Buy order");

                        // 바로 Blotter 로 주문 전송
                        BotParams.RqName = "주식주문";
                        var scr_no = utils.get_scr_no();
                        var ShortCode = code;
                        var curPrice = price;
                        var ordQty = Convert.ToInt32(Math.Truncate(1000000.0 / curPrice));
                        var ordPrice = 0;
                        var hogaGb = "03";

                        // add pending order
                        BotParams.PendingOrders.Add(ShortCode);

                        SendBuyOrder(scr_no, ShortCode, ordQty, ordPrice, hogaGb);

                        // sleep 2 sec after Send buy order.
                        utils.delay(2000);
                    }
                }

                // 매도
                if (BotParams.Accnt_Position.ContainsKey(code) && BotParams.BlotterStateDict.ContainsKey(code))
                {
                    var shortCode = code;
                    var curPrice = price.ToString();
                    var buyPrice = BotParams.Accnt_Position[shortCode].position_BuyPrice;
                    var balanceQty = BotParams.Accnt_Position[shortCode].position_BalanceQty;
                    var positionChange = BotParams.Accnt_Position[shortCode].position_Change;

                    // Monitoring sell signals
                    strategy1.MonitoringSellSignals(shortCode, curPrice, buyPrice, positionChange);

                    // 매도 주문
                    if (BotParams.SellSignals[code] == true && !BotParams.PendingOrders.Contains(code))
                    {
                        BotParams.RqName = "주식주문";
                        var scr_no = utils.get_scr_no();
                        var ordPrice = 0;
                        var hogaGb = "03";

                        // add Pending order
                        BotParams.PendingOrders.Add(shortCode);

                        SendSellOrder(scr_no, code, Convert.ToInt32(balanceQty), ordPrice, hogaGb);

                        // 해당 종목 매도 시 다시 Tick Avg 계산.
                        strategy1.ResetTickDataList(code);
                    }
                }
            }

            // B06
            if (e.sRealType == "주식시세")
            { }
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
                    var OrderTime = API.GetChejanData(908).Trim().ToString();
                    OrderTime = OrderTime.Substring(0, 2) + ":" + OrderTime.Substring(2, 2) + ":" + OrderTime.Substring(4, 2);
                    var orderNmumber = API.GetChejanData(9203).Trim().ToString();
                    var ShortCode = API.GetChejanData(9001).Trim().ToString();
                    ShortCode = ShortCode.Replace("A", "");
                    var KrName = API.GetChejanData(302).Trim().ToString();
                    var OrderType = API.GetChejanData(913).Trim().ToString();
                    var Type = API.GetChejanData(905).Trim().ToString();    // 매수 or 매도
                    var OrderQty = API.GetChejanData(900).Trim().ToString();    // 주문상태
                    var FilledQty = API.GetChejanData(911).Trim().ToString();
                    var OrderPrice = API.GetChejanData(901).Trim().ToString();
                    var FilledPrice = API.GetChejanData(910).Trim().ToString();

                    var CurPrice = API.GetChejanData(10).Trim().ToString();

                    // OrderNumber && OrderType.
                    var orderNumberAndOrderType = new List<string>() { orderNmumber, OrderType };

                    // Display blotter.
                    if (!BotParams.OrderNumberAndOrderType.ContainsKey(orderNumberAndOrderType))
                    {
                        BotParams.BltList = new List<string> { OrderTime, orderNmumber, ShortCode, KrName, OrderType, Type, OrderQty, FilledQty, OrderPrice, FilledPrice, BotParams.CurTime };
                        bltScreen.DisplayBLT(BotParams.BltList);

                        BotParams.OrderNumberAndOrderType[orderNumberAndOrderType] = ShortCode;              
                    }

                    // 체결 + 매수
                    if (!BotParams.OrderedStocks.Contains(ShortCode) && OrderType == "체결" && Type == "+매수")
                    {
                        // Remove Pending Orders
                        if (BotParams.PendingOrders.Contains(ShortCode))
                            BotParams.PendingOrders.Remove(ShortCode);

                        // TickList, TickOneMinList, BeforeAvg Dictionary 초기화
                        strategy1.ResetTickDataList(ShortCode);

                        var change_in_case_0 = (double.Parse(CurPrice) / double.Parse(FilledPrice));
                        var tradingPnL_in_case_0 = (double.Parse(CurPrice) - double.Parse(FilledPrice)) * double.Parse(FilledQty);

                        var positionState = new PositionState(ShortCode, KrName, FilledQty.ToString(), FilledPrice.ToString(), CurPrice.ToString(), change_in_case_0.ToString(), tradingPnL_in_case_0.ToString());
                        BotParams.Accnt_Position[ShortCode] = positionState;

                        // 체결 후 계좌조회
                        // GetAccountInformation();

                        telegram.SendTelegramMsg($"Type : {Type} / KrName : {KrName} / FilledPrice : {FilledPrice} / Amount : {FilledQty}");

                        // Update Blotter DIctionary
                        var blotterState = new BlotterState(OrderTime, orderNmumber, ShortCode, KrName, OrderType, Type, double.Parse(OrderQty), double.Parse(FilledQty), double.Parse(OrderPrice), double.Parse(FilledPrice));
                        BotParams.BlotterStateDict[ShortCode] = blotterState;
                        
                        BotParams.OrderedStocks.Add(ShortCode);
                        logs.write_sys_log($"OrderedStock {KrName}", 0);

                        if (BotParams.OrderingStocks.Contains(ShortCode))
                            BotParams.OrderingStocks.Remove(ShortCode);
                    }

                    // 체결 + 매도
                    if (OrderType == "체결" && BotParams.OrderedStocks.Contains(ShortCode) && Type == "-매도")
                    {
                        telegram.SendTelegramMsg($"매도 => Type : {Type} / KrName : {KrName} / FilledPrice : {FilledPrice} / Amount : {FilledQty}");
                        
                        // Remove Pending Orders
                        if (BotParams.PendingOrders.Contains(ShortCode))
                            BotParams.PendingOrders.Remove(ShortCode);

                        BotParams.Accnt_Position.Remove(ShortCode);
                        BotParams.OrderedStocks.Remove(ShortCode);
                    }


                    if (!BotParams.OrderingStocks.Contains(ShortCode) && OrderType == "접수")
                    {
                        BotParams.OrderingStocks.Add(ShortCode);
                        logs.write_sys_log($"OrderingStock {KrName}", 0);
                        telegram.SendTelegramMsg($"{KrName} 이(가) 주문접수 되었습니다.");

                        var blotterState_jubsu = new BlotterState(OrderTime, orderNmumber, ShortCode, KrName, OrderType, Type, double.Parse(OrderQty), double.Parse("0"), double.Parse(OrderPrice), double.Parse("0"));
                        BotParams.BlotterStateDict[ShortCode] = blotterState_jubsu;
                    }

                    // 매수 매도 후 잔고 갱신
                    //BLT_GetAccountInformation();

                    break;

                // 국내주식 잔고변경
                case "1":
                    BotParams.Deposit = double.Parse(API.GetChejanData(951).Trim().ToString());
                    var ShortCode1 = API.GetChejanData(9001).Trim().ToString();
                    ShortCode1 = ShortCode1.Replace("A", "");
                    var KrName1 = API.GetChejanData(302).Trim().ToString();
                    var BalanceQty = API.GetChejanData(930).Trim().ToString();
                    var BuyPrice = API.GetChejanData(931).Trim().ToString();
                    var CurPrice_in_case_1 = API.GetChejanData(10).Trim().ToString();
                    //var Change = API.GetChejanData(8019).Trim().ToString();
                    var Change = (double.Parse(CurPrice_in_case_1) / double.Parse(BuyPrice)).ToString();
                    var TradingPnL = (double.Parse(CurPrice_in_case_1) - double.Parse(BuyPrice)) * double.Parse(BalanceQty);

                    BotParams.PositionList = new List<string> { ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice_in_case_1, Change, TradingPnL.ToString() };

                    var state = new PositionState(ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice_in_case_1, Change, TradingPnL.ToString());
                    BotParams.PositionDict[ShortCode1] = state;

                    position.DisplayPosition(ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice_in_case_1, Change, TradingPnL.ToString());

                    // Display Account Information
                    BotParams.todayPnL = API.GetChejanData(990).Trim().ToString();
                    BotParams.todayChange = API.GetChejanData(991).Trim().ToString();
                    BotParams.Deposit = double.Parse(API.GetChejanData(951).Trim());

                    /* 매도 시 Mode Class를 호출하여 각각의 Mode에서의 전략으로 매도 실행 */
                    /* ver2.0 에서는 여기에 바로 매도 로직 작성 */

                    telegram.SendTelegramMsg($"국내주식 잔고변경 KrName : {KrName1} Balance : {BalanceQty} CurPrice : {CurPrice_in_case_1} Change : {Change} TradingPnL : {TradingPnL}");
                    logs.write_sys_log($"국내주식 잔고변경 KrName : {KrName1} Balance : {BalanceQty} CurPrice : {CurPrice_in_case_1} Change : {Change} TradingPnL : {TradingPnL}", 0);

                    // Test CurPrice
                    if (double.Parse(CurPrice_in_case_1) == 0.0)
                        telegram.SendTelegramMsg($"{KrName1}의 현재가가 {CurPrice_in_case_1} 입니다.");

                    break;
            }
        }

        public void LiquidationStocks()
        {

            var accntStockCodes = BotParams.Accnt_Position.Keys;

            for (int i=0; i < accntStockCodes.Count; i++)
            {
                var shortCode = BotParams.Accnt_Position[accntStockCodes[i]].position_ShortCode;
                var krName = BotParams.Accnt_Position[accntStockCodes[i]].position_KrName;
                var balance = BotParams.Accnt_Position[accntStockCodes[i]].position_BalanceQty;

                // add pending orders
                BotParams.PendingOrders.Add(shortCode);

                // Send sell order
                var scr_no = utils.get_scr_no();
                telegram.SendTelegramMsg($"[{krName}] 정리매매 주문 전송합니다. scrNo : {scr_no}");

                SendSellOrder(scr_no, shortCode, Convert.ToInt32(balance), 0, "03");

                while (true)
                {
                    if (!BotParams.PendingOrders.Contains(shortCode))
                    {
                        logs.write_sys_log($"[{krName}] 정리매매 주문 성공했습니다.", 0);
                        telegram.SendTelegramMsg($"[{krName}] 정리매매 주문 성공했습니다");

                        break;
                    }
                }
            }
        }


        public void SendBuyOrder(string scr_no, string ShortCode, int ordQty, long ordPrice, string hogaGB)
        {

            //telegram.SendTelegramMsg($"BuyOrder {ShortCode}/{ordQty}  / ShortCode length : {ShortCode.Length} ordQty length : {ordQty.ToString().Length}");

            try
            {
                // TODO : 매수잔량 취소 기능 추가
                if (!BotParams.OrderingStocks.Contains(ShortCode))
                {
                    var res = API.SendOrder("주식주문", scr_no, "8003542111", 1, ShortCode.Trim(), ordQty, 0, hogaGB, "");

                    if (res == 0)
                    {
                        telegram.SendTelegramMsg($"Success to Send BuyOrder {ShortCode}/{ordQty.ToString()}");
                    }
                    else telegram.SendTelegramMsg($"[{ShortCode}] Failed to Send BuyOrder, res : {res.ToString()}");
                }

                else telegram.SendTelegramMsg($"[{ShortCode}] have ordering List");

            }
            catch (Exception e)
            {
                telegram.SendTelegramMsg($"Exception in Send BuyOrder errMsg : {e}");
            }
        }

        public void SendSellOrder(string scr_no, string ShortCode, int ordQty, int ordPrice, string hogaGB)
        {
            try
            {
                var res = API.SendOrder("주식주문", scr_no, "8003542111", 2, ShortCode.Trim(), ordQty, 0, hogaGB, "");

                if (res == 0)
                {
                    telegram.SendTelegramMsg($"Success to Send SellOrder {ShortCode}/{ordQty.ToString()} res : {res}");           
                }

                else telegram.SendTelegramMsg($"[{ShortCode}] Failed to Send SellOrder, res : {res.ToString()}");
            }
            catch (Exception e)
            {
                telegram.SendTelegramMsg($"Exception in send SellOrder errMsg : {e.ToString()}");
            }
        }
    }
}
