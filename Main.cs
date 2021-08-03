using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace TaewooBot_v2
{


    public partial class Main : Form
    {
        // Initialize global params
        public string Market { get; set; }
        public string UserID { get; set; }
        public int ScrNo { get; set; }
        public bool IsThread { get; set; }
        public bool _SearchCondition { get; set; }
        public bool _GetTrData { get; set; }
        public bool _GetRTD { get; set; }
        public string RqName { get; set; }
        public string CurTime { get; set; }

        public bool Test { get; set; } = false;

        // About Account
        public string AccountNumber { get; set; } = null;
        public double Deposit { get; set; } = 0.0;
        public int AccountStockLots { get; set; }
        public int TotalPnL { get; set; }

        // Slow Params
        public double LossCut { get; set; }

        // Logs File
        public string date { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        public string Path { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/Log/";
        public string TickPath { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/Log/TickLog/";
        public string LogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_Log.txt";
        public string TickLogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_TickLog.txt";

        // Stocks
        List<string> TargetCodes = new List<string>();
        public string[] Codes;
        public string Kospi { get; set; } = null;
        public string Kosdaq { get; set; } = null;
        public string AllMarket { get; set; } = null;

        // Thread 간 동기화
        public bool signal { get; set; } = false;
        public bool order { get; set; } = false;
        public string SignalStockCode { get; set; } = null;
        public string SignalKrName { get; set; } = null;
        public string SignalPrice { get; set; } = null;

        // Delegate winforms
        delegate void Ctr_Involk(Control ctr, string text);


        // Test
        public int StockCnt { get; set; } = 0;

        // 항목별 DIctionary 설정

        Dictionary<string, string> targetDict = new Dictionary<string, string>();
        Dictionary<string, string> StockKrNameDict = new Dictionary<string, string>();
        Dictionary<string, string> StockPriceDict = new Dictionary<string, string>();
        Dictionary<string, string> TickSpeedDict = new Dictionary<string, string>();
        Dictionary<string, string> StockPnLDict = new Dictionary<string, string>();
        Dictionary<string, string> StockHighPriceDict = new Dictionary<string, string>();
        
        Dictionary<string, List<string>> TickAvgDict = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> StockDict = new Dictionary<string, List<string>>();


        // 계좌관련 Dictionary 설정
        Dictionary<string, string> Accnt_StockName = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockLots = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockPnL = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockPnL_Won = new Dictionary<string, string>();


        Thread GetDataThread = null;
        Thread MonitoringSignalThread = null;
        Thread OrderThread = null;

        // Instance Other WinForms
        Logs logs = new Logs();
        Params Params = new Params();
        Universe universe = new Universe();
        Position position = new Position();


        public Main()
        {
            InitializeComponent();
            /*
            this.API.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(this.DKHOpenAPI1_OnReceiveTrData);
            this.API.OnReceiveMsg += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEventHandler(this.DKHOpenAPI1_OnReceiveMsg);
            this.API.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(this.DKHOpenAPI1_OnReceiveChejanData);
            this.API.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(this.DKHOpenAPI1_OnReceiveRealData);
            */

            // Open Windows
            logs.Show();
            universe.Show();
            position.Show();

            // Initialize Parameters
            InitialParams();

            // Make Log text files
            MakeLogFile();
            MakeTickDataFile();

            // Tr code 검색
            this.API.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(this.OnReceiveTrData);
            this.API.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(this.OnReceiveRealData);

            // 조건검색
            this.API.OnReceiveConditionVer += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEventHandler(this.OnReceiveConditionVer);
            this.API.OnReceiveTrCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEventHandler(this.OnReceiveTrCondition);
            this.API.OnReceiveRealCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEventHandler(this.OnReceiveRealCondition);

        }

        private void MarketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MarketType.Focused != true)
            {
                return;
            }
            else
            {
                Market = MarketType.Text;
            }
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {

            if (Market == "Stock")
            {
                write_sys_log(Market, 0);
                Login();


                // 자동매매 Thread 시작
                if (IsThread is true) // 스레드가 이미 생성된 상태라면
                {
                    write_sys_log("AUTO TRADING SYSTEM is on already. \n", 0);
                    return;
                }

                write_sys_log("AUTO TRADING SYSTEM is just started \r\n", 0);
                IsThread = true;
                GetDataThread = new Thread(new ThreadStart(GetData));
                MonitoringSignalThread = new Thread(new ThreadStart(MonitoringSignal));
                GetDataThread.Start();
                MonitoringSignalThread.Start();
            }
        }

        // Thread1 (GetData Thread)
        public void GetData()
        {
     
            if (TestCheck.Checked)
            {
                Test = true;
            }

            for (; ; )  // 장전 30분 무한루프 실행
            {

                CurTime = get_cur_tm(); // 현재시각 조회
                CurrentTime.Text = CurTime; // 화면 하단 상태란에 메시지 출력

                if (CurTime.CompareTo("08:30:01") >= 0 && CurTime.CompareTo("09:00:00") < 0)
                {
                    
                }

                else if (CurTime.CompareTo("09:00:00") >= 0 && CurTime.CompareTo("15:19:59") < 0)
                {
                    // Step1. 조건검색 시작
                    if (_SearchCondition is false)
                    {

                        GetAccountInformation();

                        _SearchCondition = true;
                        write_sys_log("Start SearchCondition", 0);
                        
                        // 조건검색 종목
                        //API.GetConditionLoad();

                        // 전 종목
                        RequestAllStocks();
                    }
                    
                    for (; ; )  // 장 중 무한루프 실행
                    {
                        // Test
                        // break;

                        setText_Control(GetDataTextBox, "running");
                        
                        CurTime = get_cur_tm();
                        CurrentTime.Text = CurTime; // 화면 하단 상태란에 메시지 출력

                        if (CurTime.CompareTo("15:19:50") >= 0)
                        {
                            // 여기서 모든 데이터 전송 Disconnect 하기.
                            write_sys_log("End of Today's Trade!", 0);
                            InitialParams();
                            break;
                        }
                        delay(1000);
                    }
                    break;
                }

                // After Market for test
                else if (Test == true)
                {
                    GetAccountInformation();
                    RequestAllStocks();
                    setText_Control(GetDataTextBox, "running");

                    Test = false;

                }

                delay(200);
            }
        }

        // Thread2 (Monitoring Thread)
        public void MonitoringSignal()
        {
            while(true)
            {

                setText_Control(MonitoringTextBox, "Singal is false");

                if (signal == true)
                {

                    setText_Control(MonitoringTextBox, "Singal is true");

                    write_sys_log("Get the MonitroingSignalThread in here", 0);

                    DisplayPosition(SignalStockCode, SignalKrName, SignalPrice, "", "Buy");

                    order = true;

                    // 만약 한종목만 사면 여기서 
                    // singal을 다시 false로 풀어주는게 아니라
                    // order thread에서 Sell Order 가 끝나고 나서 signal, order flag를 false 로 풀어준다.

                }

            }
        }


        public void Order()
        {
            // 계좌상황 체크
            GetAccountInformation();

            while (true)
            {
                setText_Control(OrderTextBox, "Order is true");
                if (order == true)
                {
                    // API OCX를 따로 두고 여기서 계속 매도 조건 Monitoring 하기.
                    if (AccountStockLots != 0)
                    {
                        setText_Control(OrderTextBox, "BuyOrder is true");                        
                        // 매도까지 기다리기.
                        //MonitoringSellStocks();
                    }
                    else
                    {
                        setText_Control(OrderTextBox, "SellOrder is true");
                        // 조건에 맞는 종목 매수하기

                        //MonitoringBuyStocks();
                    }
                }

            }

        }

        private void TestCheck_CheckedChanged(object sender, EventArgs e)
        {

        }


    }

    class ConditionInfo
    {
        public int ConditionNum { get; set; }
        public String ConditionNm { get; set; }
        public Boolean ReqRealTime = true;

        //public List<StockItemInfo> stockItemList;
    }

    public class StockInfo
    {
        public string Code { get; set; }
        public string KrName { get; set; }
        public string Price { get; set; }
    }

    class StockValueInfo
    {
        public string Code { get; set; }
        public string KrName { get; set; }
        public int BuyPrice { get; set; }
        public int ValuePrice { get; set; }
        public double ValueRate { get; set; }
        public int Inventory { get; set; }
    }
}
