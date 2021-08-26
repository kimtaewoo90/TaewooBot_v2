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

        //private Dictionary<string, List<int>> TickList = new Dictionary<string, List<int>>();
        //private Dictionary<string, List<int>> TickOneMinsList = new Dictionary<string, List<int>>();


        // KiwoomEvents
        List<ConditionInfo> conditionList;

        // Send Condition
        private void OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            conditionList = new List<ConditionInfo>();

            string condtionNameList = API.GetConditionNameList();
            string[] conditionNameArray = condtionNameList.Split(';');

            for (int i = 0; i < conditionNameArray.Length; i++)
            {
                string[] ConditionInfo = conditionNameArray[i].Split('^');
                if (ConditionInfo.Length == 2)
                {
                    conditionList.Add(new ConditionInfo()
                    {
                        ConditionNum = int.Parse(ConditionInfo[0].Trim()),
                        ConditionNm = ConditionInfo[1].Trim()
                    });
                }
            }

            API.SendCondition(utils.get_scr_no(), conditionList[0].ConditionNm, conditionList[0].ConditionNum, 1);


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
            conditionList = new List<ConditionInfo>();

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
                    universe.DisplayTargetStocks("Insert", e.sTrCode.ToString(), stockName, "0", "0", "0");
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
            if (BotParams.RqName.CompareTo(e.sRQName) != 0)
            {
                logs.write_sys_log("Should check the TRCode", 0);
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

            if (e.sRQName == "현재가조회")
            {
                 API.CommGetData(e.sTrCode, "", e.sRQName, 0, "현재가");

            }

            if (e.sRQName == "주식기본정보")
            {
                string Code = API.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim().ToString();
                string KrName = API.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim().ToString();
                string Price = API.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim().ToString();

                universe.DisplayTargetStocks("Insert", Code, KrName, Price, "0", "0");
                logs.write_sys_log($"[{BotParams.StockCnt}번째] {KrName}의 주식기본정보를 받아오는데에 성공하였습니다.", 0);

                // Update Dict
                try
                {
                    List<int> arr = new List<int>();
                    var state = new StockState(Code, KrName, Price, "0", "0", "0", "0", arr, arr, 0.0, DateTime.Parse(BotParams.CurTime));
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
                for (int nIdx = 0; nIdx < BotParams.AccountStockLots; nIdx++)
                {
                    string code = API.GetCommData(e.sTrCode, e.sRQName, BotParams.AccountStockLots, "종목코드").Trim();
                    BotParams.Accnt_StockName[code] = API.GetCommData(e.sTrCode, e.sRQName, BotParams.AccountStockLots, "종목명").Trim();
                    BotParams.Accnt_StockLots[code] = API.GetCommData(e.sTrCode, e.sRQName, BotParams.AccountStockLots, "보유수량").Trim();
                    BotParams.Accnt_StockPnL[code] = API.GetCommData(e.sTrCode, e.sRQName, BotParams.AccountStockLots, "손익율").Trim();
                    BotParams.Accnt_StockPnL_Won[code] = API.GetCommData(e.sTrCode, e.sRQName, BotParams.AccountStockLots, "손익금액").Trim();

                    string msg = $"종목명 : {BotParams.Accnt_StockName[code]}, 보유수량 : {BotParams.Accnt_StockLots[code]}, 손익율 : {BotParams.Accnt_StockLots[code]}, 손익금액 : {BotParams.Accnt_StockPnL_Won}";

                    logs.write_sys_log(msg, 0);
                }

                if (BotParams.AccountStockLots == 0)
                {
                    logs.write_sys_log("보유종목이 없습니다.", 0);
                }
            }
        }

        // Get Real Data
        private void OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            // A03
            if (e.sRealType == "주식체결")
            {
                string Code = e.sRealKey;
                string KrName = GetKrName(Code);
                double Price = Math.Abs(double.Parse(API.GetCommRealData(e.sRealType, 10).Trim()));  // current price;
                double Change = double.Parse(API.GetCommRealData(e.sRealType, 12));
                string ContractLots = API.GetCommRealData(e.sRealType, 15).Trim().ToString(); // 체결량
                //double buy_price = get_hoga_unit_price((int)Price, Code, -2);
                double highPrice = double.Parse(API.GetCommRealData(e.sRealType, 17).Trim());
                double lowPrice = double.Parse(API.GetCommRealData(e.sRealType, 18).Trim());
                string TickTime = API.GetCommRealData(e.sRealType, 20).Trim().ToString();

                double beforeAvg = 0;


                // Total Tick List
                if (!BotParams.TickList.ContainsKey(Code))
                { 
                    beforeAvg = 0;
                    List<int> tempTickList = new List<int>() { Convert.ToInt32(ContractLots) };
                    BotParams.TickList[Code] = tempTickList;
                }

                else
                { 
                    beforeAvg = BotParams.TickList[Code].Average();
                    BotParams.TickList[Code].Add(Convert.ToInt32(ContractLots));
                }

                // 1 Min Tick List
                if (!BotParams.TickOneMinsList.ContainsKey(Code))
                {
                    List<int> tempList = new List<int>() {Convert.ToInt32(ContractLots)};
                    BotParams.TickOneMinsList[Code] = new List<int> { Convert.ToInt32(ContractLots) };
                }
                else
                {
                    BotParams.TickOneMinsList[Code].Add(Convert.ToInt32(ContractLots));
                }

                double lowOneMinPrice = double.Parse(BotParams.stockState[Code].states_lowPrice);
                if (lowOneMinPrice > Price) lowOneMinPrice = Price;

                // Compare Time for 1 MIn
                if (DateTime.Parse(BotParams.CurTime) > compareTime.AddMinutes(1))
                {
                    compareTime = DateTime.Parse(BotParams.CurTime);
                    if(BotParams.TickOneMinsList.ContainsKey(Code))
                    {
                        BotParams.TickOneMinsList.Remove(Code);
                        BotParams.TickOneMinsList[Code] = new List<int> { Convert.ToInt32(ContractLots) };
                    }

                    lowOneMinPrice = Price;
                }

                // Update stockState Dictionary
                var state = new StockState(Code, 
                                           KrName, 
                                           Price.ToString(), 
                                           highPrice.ToString(),
                                           lowOneMinPrice.ToString(),
                                           Change.ToString(), 
                                           ContractLots, 
                                           BotParams.TickList[Code], 
                                           BotParams.TickOneMinsList[Code], 
                                           beforeAvg, 
                                           DateTime.Parse(BotParams.CurTime));
                BotParams.stockState[Code] = state;         

                // Save TickLog.txt
                File.AppendAllText(BotParams.TickPath + BotParams.TickLogFileName, $"[{BotParams.CurTime}] : {Code} / {Price.ToString()} / {Change.ToString()} / {ContractLots}" + Environment.NewLine, Encoding.Default);

                // Display the RTD data on TargetStocks DataGridView
                universe.DisplayTargetStocks("Update", Code, "", Price.ToString(), BotParams.TickOneMinsList[Code].Average().ToString(), Change.ToString());

                bool orderStocks = state.MonitoringSignals();
                
                if (orderStocks is true)
                {
                    if (BotParams.Deposit > 1000000 && BotParams.Accnt_StockName.ContainsKey(Code) is false)
                    {

                        logs.write_sys_log($"{Code} Signal is true", 0);

                        telegram.SendTelegramMsg($"[{Code}/{KrName}] try to send Buy order");

                        // 바로 Blotter 로 주문 전송
                        BotParams.RqName = "주식주문";
                        var scr_no = utils.get_scr_no();
                        var ShortCode = Code;
                        var curPrice = Price;
                        var ordQty = Convert.ToInt32(Math.Truncate(1000000.0 / curPrice));
                        var ordPrice = 0;
                        var hogaGb = "03";

                        bltScreen.SendBuyOrder(scr_no, ShortCode, ordQty, ordPrice, hogaGb);

                        // reset signal.
                        state.signal_1 = false;
                        state.signal_2 = false;
                        state.signal_3 = false;
                    }
                }
            }

            // B06
            if (e.sRealType == "주식시세")
            {
                
            }


        }


    }
}
