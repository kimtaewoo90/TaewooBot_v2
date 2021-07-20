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
        public bool OrderThread { get; set; } = false;
        public bool _SearchCondition { get; set; }
        public bool _GetTrData { get; set; }
        public bool _GetRTD { get; set; }
        public string RqName { get; set; }

        // About Account
        public string AccountNumber { get; set; } = null;
        public double Deposit { get; set; } = 0.0;
        public int AccountStockLots { get; set; }

        // Slow Params
        public double LossCut { get; set; }


        List<string> TargetCodes = new List<string>();


        // 항목별 DIctionary 설정

        Dictionary<string, string> targetDict = new Dictionary<string, string>();
        Dictionary<string, string> StockKrNameDict = new Dictionary<string, string>();
        Dictionary<string, string> StockPriceDict = new Dictionary<string, string>();
        Dictionary<string, string> TickSpeedDict = new Dictionary<string, string>();
        Dictionary<string, string> StockPnLDict = new Dictionary<string, string>();

        // 계좌관련 Dictionary 설정
        Dictionary<string, string> Accnt_StockName = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockLots = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockPnL = new Dictionary<string, string>();
        Dictionary<string, string> Accnt_StockPnL_Won = new Dictionary<string, string>();

        Thread StockInfoThread = null;
        Thread Orders = null;


        public Main()
        {
            InitializeComponent();
            /*
            this.API.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(this.DKHOpenAPI1_OnReceiveTrData);
            this.API.OnReceiveMsg += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEventHandler(this.DKHOpenAPI1_OnReceiveMsg);
            this.API.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(this.DKHOpenAPI1_OnReceiveChejanData);
            this.API.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(this.DKHOpenAPI1_OnReceiveRealData);
            */

            InitialParams();
            MakeLogFile();

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
                StockInfoThread = new Thread(new ThreadStart(m_thread1));
                StockInfoThread.Start();

            }
        }

        public void m_thread1()
        {
            string CurTime = null;

            for (; ; )  // 장전 30분 무한루프 실행
            {

                CurTime = get_cur_tm(); // 현재시각 조회
                CurrentTime.Text = CurTime; // 화면 하단 상태란에 메시지 출력



                if (CurTime.CompareTo("08:30:01") >= 0 && CurTime.CompareTo("09:00:00") < 0)
                {
                    
                }

                else if (CurTime.CompareTo("09:00:00") >= 0 && CurTime.CompareTo("15:19:59") < 0)
                {
                    // 장 시작 OrderThread 시작
                    OrderThread = true;
                    Orders = new Thread(new ThreadStart(m_OrderThread));
                    Orders.Start();

                    // Step1. 조건검색 시작
                    if (_SearchCondition is false)
                    {
                        _SearchCondition = true;
                        write_sys_log("Start SearchCondition", 0);
                        API.GetConditionLoad();
                    }
                    
                    for (; ; )  // 장 중 무한루프 실행
                    {
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

                else
                {

                }

                delay(200);
            }
        }

        public void m_OrderThread()
        {
            // 계좌상황 체크
            GetAccountInformation();

            while (true)
            {
                if (AccountStockLots != 0)
                {
                    // 매도까지 기다리기.
                    MonitoringSellStocks();
                }
                else
                {
                    // 조건에 맞는 종목 매수하기
                    MonitoringBuyStocks();
                }
            }

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
