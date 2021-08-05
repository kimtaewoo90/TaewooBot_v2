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
                utils.write_sys_log("검색된 조건목록에 대한 종목이 없습니다.\n", 0);
            }
        }
        private void OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            conditionList = new List<ConditionInfo>();

            //종목 편입
            if (e.strType.Equals("I"))
            {
                string stockName = API.GetMasterCodeName(e.sTrCode);
                utils.write_sys_log("편입종목 : " + "[" + stockName + " ].\n", 0);

                string scr_no = null;
                scr_no = "";
                scr_no = utils.get_scr_no();

                string msg = "편입종목 : " + stockName;

                utils.write_sys_log(msg, 0);

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
                    utils.DisplayTargetStocks("Insert", e.sTrCode.ToString(), stockName, "0", "0", "0");
                }



                utils.delay(1000);


                // 추가된 종목만 따로 실시간 요청
                //ReqRealData(e.sTrCode, scr_no);


                // 주가 데이터 요청.
                botParams.RqName = "";
                botParams.RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", e.sTrCode);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(botParams.RqName, "OPT10001", 0, scr_no);
            }

            //종목 이탈
            else if (e.strType.Equals("D"))
            {
                string stockName = API.GetMasterCodeName(e.sTrCode);
                utils.write_sys_log("이탈종목 : " + "[" + stockName + " ].\n", 0);

                //string scr_no = targetDict[e.sTrCode];
                //API.DisconnectRealData(scr_no);

                int i = 0;
                for (i = 0; i < botParams.TargetCodes.Count; i++)
                {
                    if (botParams.TargetCodes[i].Equals(e.sTrCode))
                    {
                        botParams.TargetCodes.RemoveAt(i);
                        //break;
                    }
                }

                botParams.TargetCodes.Remove(e.sTrCode);

                // Remove 이탈Stock in Dictionary
                utils.RemoveDict(e.sTrCode);

                utils.write_sys_log("이탈종목 : " + stockName, 0);

                //DeleteTargetStocks(e.sTrCode.ToString());
            }

            utils.write_sys_log(botParams.TargetCodes.ToString(), 0);

        }

        // Get TR Data
        private void OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (botParams.RqName.CompareTo(e.sRQName) != 0)
            {
                utils.write_sys_log("Should check the TRCode", 0);
            }
            else
            {
                switch (botParams.RqName)
                {
                    // OPW00001
                    case "예수금상세현황요청":
                        utils.write_sys_log("예수금상세현황 요청", 0);
                        break;

                    // OPW00004
                    case "계좌평가현황요청":
                        utils.write_sys_log("계좌평가현황 요청", 0);
                        break;

                    case "호가조회":
                        utils.write_sys_log("호가조회 요청", 0);
                        break;

                    case "현재가조회":
                        utils.write_sys_log("현재가조회 요청", 0);
                        break;

                    case "주식시세":  // for 거래량 조회
                        utils.write_sys_log("주식시세 요청", 0);
                        
                        break;

                    case "주식기본정보":  // for 주식기본정보
                        utils.write_sys_log("주식기본정보 요청", 0);
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

                utils.DisplayTargetStocks("Insert", Code, KrName, Price, "0", "0");
                utils.write_sys_log($"[{botParams.StockCnt}번째] {KrName}의 주식기본정보를 받아오는데에 성공하였습니다.", 0);

                // Update Dict
                try
                {
                    botParams.TargetCodes.Add(Code);
                    botParams.StockKrNameDict.Add(Code, KrName);
                    botParams.StockPriceDict.Add(Code, Price);
                }
                catch (Exception err)
                {
                    utils.write_sys_log(err.ToString(), 0);
                }



                // StockInfo class의 instance가 동적으로 안되면 각 항목마다 DIct를 줘서 비교.

                // TODO : Save the Stock Data at Dictionary in here.

                utils.write_sys_log("주식기본정보 받기 성공", 0);
                
            }

            if (e.sRQName == "조건검색종목")
            {
                int count = API.GetRepeatCnt(e.sTrCode, e.sRQName); //요청의 반복 횟수를 요청합니다.
                utils.write_sys_log("조건검색종목 요청", 0);

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
                botParams.Deposit = Double.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim());
                utils.write_sys_log(botParams.Deposit.ToString(), 0);
            }

            if (e.sRQName == "계좌평가현황요청")
            {
                botParams.Deposit = Double.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim());
                botParams.TotalPnL = Int32.Parse(API.GetCommData(e.sTrCode, e.sRQName, 0, "당일손익율").Trim());


                setText_Control(AccountTextBox, botParams.AccountNumber.ToString());
                setText_Control(DepositTextBox, botParams.Deposit.ToString());
                setText_Control(TotalPnLTextBox, botParams.TotalPnL.ToString());

                botParams.AccountStockLots = API.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int nIdx = 0; nIdx < botParams.AccountStockLots; nIdx++)
                {
                    string code = API.GetCommData(e.sTrCode, e.sRQName, botParams.AccountStockLots, "종목코드").Trim();
                    botParams.Accnt_StockName[code] = API.GetCommData(e.sTrCode, e.sRQName, botParams.AccountStockLots, "종목명").Trim();
                    botParams.Accnt_StockLots[code] = API.GetCommData(e.sTrCode, e.sRQName, botParams.AccountStockLots, "보유수량").Trim();
                    botParams.Accnt_StockPnL[code] = API.GetCommData(e.sTrCode, e.sRQName, botParams.AccountStockLots, "손익율").Trim();
                    botParams.Accnt_StockPnL_Won[code] = API.GetCommData(e.sTrCode, e.sRQName, botParams.AccountStockLots, "손익금액").Trim();

                    string msg = $"종목명 : {botParams.Accnt_StockName[code]}, 보유수량 : {botParams.Accnt_StockLots[code]}, 손익율 : {botParams.Accnt_StockLots[code]}, 손익금액 : {botParams.Accnt_StockPnL_Won}";

                    utils.write_sys_log(msg, 0);
                }

                if (botParams.AccountStockLots == 0)
                {
                    utils.write_sys_log("보유종목이 없습니다.", 0);
                }
            }
        }

        // Get Real Data
        private void OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            // A03
            if (e.sRealType == "주식체결")
            {
                int Volume = 0;
                Volume = int.Parse(API.GetCommRealData(e.sRealType, 13)); // 누적거래량

                string Code = e.sRealKey;
                string KrName = GetKrName(Code);
                double Price = Math.Abs(double.Parse(API.GetCommRealData(e.sRealType, 10).Trim()));  // current price;
                double UpDownRate = double.Parse(API.GetCommRealData(e.sRealType, 12));
                string ContractLots = API.GetCommRealData(e.sRealType, 15).Trim().ToString(); // 체결량
                //double buy_price = get_hoga_unit_price((int)Price, Code, -2);
                double highPrice = double.Parse(API.GetCommRealData(e.sRealType, 17).Trim());
                string TickTime = API.GetCommRealData(e.sRealType, 20).Trim().ToString();

                botParams._GetRTD = true;

                // Update Dictionary
                botParams.StockPriceDict[Code] = Price.ToString();
                botParams.StockPnLDict[Code] = UpDownRate.ToString();
                botParams.StockKrNameDict[Code] = KrName.ToString();
                botParams.TickSpeedDict[Code] = ContractLots.ToString();
                botParams.StockHighPriceDict[Code] = highPrice.ToString();

                List<string> TickAvgList = new List<string>();
                TickAvgList.Add(TickTime.ToString());
                TickAvgList.Add(ContractLots.ToString());
                botParams.TickAvgDict[Code] = TickAvgList;

                // 분당 거래대금

                // TODO : How To Calculate average Tick Speed in 1 Minute
                
                // 틱 속도
                string TickAvgBefore;
                string TickAvgNow;
                if (botParams.StockDict.Keys.Contains(Code))
                {
                    TickAvgBefore = botParams.StockDict[Code][5];
                    TickAvgNow = ContractLots.ToString();
                }
                else
                {
                    TickAvgNow = ContractLots.ToString();
                }
                
                
                // Add Stock Info List
                List<string> StockInfoList = new List<string>();
                
                StockInfoList.Add(TickTime.ToString());
                StockInfoList.Add(KrName.ToString());
                StockInfoList.Add(UpDownRate.ToString());
                StockInfoList.Add(highPrice.ToString());
                StockInfoList.Add(Price.ToString());
                StockInfoList.Add(TickAvgNow.ToString()); ;

                // StockDict Value Index : [0] : TickTime [1] : StockKrName, [2] : UpDownRate, [3] : HgihPrice, [4]: CurPrice, [5] : TickAverage, [6] : TradingAmtperMin
                botParams.StockDict[Code] = StockInfoList;

                // TickLog 기록
                File.AppendAllText(botParams.TickPath + botParams.TickLogFileName, $"[{botParams.CurTime}] : {Code} / {Price.ToString()} / {UpDownRate.ToString()} / {ContractLots}" + Environment.NewLine, Encoding.Default);

                // ToDo : GetTickSpeed in here
                // GetTickSpeed(Code, ContractLots);

                // Display the RTD data on TargetStocks DataGridView
                utils.DisplayTargetStocks("Update", Code, "", Price.ToString(), botParams.TickSpeedDict[Code], UpDownRate.ToString());


                // signal True Conditions
                if(int.Parse(ContractLots) > 1000)
                {
                    botParams.SignalStockCode = Code;
                    botParams.SignalKrName = KrName;
                    botParams.SignalPrice = Price.ToString();

                    botParams.signal = true;
 

                    // TODO : 여기서 GetDataThread를 중지시켜야하나?
                }
            }


            if (e.sRealType == "주식시세")
            {
                // 체결 시 들어옴.
            }


        }


    }
}
