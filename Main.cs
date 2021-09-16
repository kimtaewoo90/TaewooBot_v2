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
using Newtonsoft.Json.Linq;


using System.Threading;

namespace TaewooBot_v2
{


    public partial class Main : Form
    {
        // Thread 선언
        Thread GetTime = null;
        Thread GetDataThread = null;
        Thread AccountStatusThread = null;

        //Thread MonitoringSignalThread = null;
        //Thread BlotterThread = null;
        //Thread PositionThread = null;

        // Instance Other WinForms
        Logs logs = new Logs();
        Params Params = new Params();
        Universe universe = new Universe();
        Position position = new Position();
        Blotter bltScreen = new Blotter();
        Utils utils = new Utils();

        // Coin Thread
        //Thread CoinThread = null;

        TelegramClass telegram = new TelegramClass();
        Strategy.Strategy1 strategy1 = new Strategy.Strategy1();

        delegate void Ctr_Involk(Control ctr, string text);

        public Main()
        {
            InitializeComponent();

            version.Text = "version : " + BotParams.version;

            // Initialize Parameters
            utils.InitialParams();

            // Make Log text files
            logs.MakeLogFile();
            logs.MakeTickDataFile();

            // Tr code 검색
            this.API.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(this.OnReceiveTrData);
            this.API.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(this.OnReceiveRealData);
            this.API.OnReceiveMsg += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEventHandler(this.OnReceiveMsg);

            // 조건검색
            this.API.OnReceiveConditionVer += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEventHandler(this.OnReceiveConditionVer);
            this.API.OnReceiveTrCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEventHandler(this.OnReceiveTrCondition);
            this.API.OnReceiveRealCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEventHandler(this.OnReceiveRealCondition);

            // 주식 매수 매도
            this.API.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(this.OnReceiveChejanData);

        }

        private void MarketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MarketType.Focused != true)
            {
                return;
            }
            else
            {
                BotParams.Market = MarketType.Text;
            }
        }

        // 로그인 함수 구현
        public void Login()
        {
            int ret = 0;
            int ret2 = 0;

            String accno_cnt = null;
            string[] accno_arr = null;

            ret = API.CommConnect(); // 로그인 창 호출

            Status.Text = "로그인 중..."; // 화면 하단 상태란에 메시지 출력

            for (; ; )
            {
                ret2 = API.GetConnectState(); // 로그인 완료 여부를 가져옴
                if (ret2 == 1)
                {
                    // 로그인 성공
                    break;
                }
                else
                {
                    // 로그인 대기
                    utils.delay(1000); // 1초 지연
                }
            } // end for

            Status.Text = "로그인 완료"; // 화면 하단 상태란에 메시지 출력

            BotParams.UserID = "";
            BotParams.UserID = API.GetLoginInfo("USER_ID").Trim(); // 사용자 아이디를 가져와서 클래스 변수(전역변수)에 저장
            //textBox1.Text = g_user_id; // 전역변수에 저장한 아이디를 텍스트박스에 출력

            accno_cnt = "";
            accno_cnt = API.GetLoginInfo("ACCOUNT_CNT").Trim(); // 사용자의 증권계좌 수를 가져옴

            // TODO : Error
            accno_arr = new string[int.Parse(accno_cnt)];

            BotParams.AccountNumber = API.GetLoginInfo("ACCNO").Trim().Replace(";", "");

            accno_arr = BotParams.AccountNumber.Split(';');  // API에서 ';'를 구분자로 여러개의 계좌번호를 던져준다.
            logs.write_sys_log("Account Number : " + BotParams.AccountNumber, 0);
            logs.write_sys_log("Welcome " + BotParams.UserID, 0);


        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {

            if (BotParams.Market == "Stock")
            {
                // Open Windows
                logs.StartPosition = FormStartPosition.Manual;
                logs.Location = new Point(755, 520);
                logs.Show();

                universe.StartPosition = FormStartPosition.Manual;
                universe.Location = new Point(100, 520);
                universe.Show();

                bltScreen.StartPosition = FormStartPosition.Manual;
                bltScreen.Location = new Point(860, 100);
                bltScreen.Show();

                position.StartPosition = FormStartPosition.Manual;
                position.Location = new Point(100, 100);
                position.Show();

                logs.write_sys_log(BotParams.Market, 0);

                // 자동매매 Thread 시작
                if (BotParams.IsThread is true) // 스레드가 이미 생성된 상태라면
                {
                    logs.write_sys_log("AUTO TRADING SYSTEM is on already. \n", 0);
                    return;
                }

                Login();

                logs.write_sys_log("AUTO TRADING SYSTEM is just started \r\n", 0);
                BotParams.IsThread = true;

                // Main Thread
                GetDataThread = new Thread(new ThreadStart(GetData));

                // Blotter Thread
                // BlotterThread = new Thread(new ThreadStart(BlotterDisplay));

                // Position Thread
                // PositionThread = new Thread(new ThreadStart(PositionDisplay));

                // AccountStatus Thread
                AccountStatusThread = new Thread(new ThreadStart(AccountStatus));
                
                // Get global time Thread.
                GetTime = new Thread(new ThreadStart(GetCurrentTime));

                GetTime.Start();
                GetDataThread.Start();
                AccountStatusThread.Start();

                //BlotterThread.Start();
               // PositionThread.Start();
                //MonitoringSignalThread.Start();
            }

            else if (BotParams.Market == "Coin")
            {
            } 
        }

        // Account Status Thread
        public void AccountStatus()
        {
            while (true)
            {
                var deposit = BotParams.Deposit;
                var todayChnage = BotParams.todayChange;
                var todayPnL = BotParams.todayPnL;

                BotParams.AccountList = new List<string> { BotParams.todayPnL.ToString(), BotParams.todayChange.ToString(), BotParams.Deposit.ToString() };

                position.DisplayAccount(BotParams.AccountList);
            }
        }

        // GetTime Thread
        public void GetCurrentTime()
        {
            while (true)
            {
                BotParams.CurTime = utils.get_cur_tm();
                // Display time
                CurrentTime.Text = BotParams.CurTime;
            }
        }


        // Thread1 (GetData Thread)
        public void GetData()
        {

            telegram.SendTelegramMsg("GetData Thread is started");

            var batchData = false;
            var IsLiquidation = true;
            BotParams.IsLiquidation = true;

            // while 문으로 무한루프 & 시간계산
            while (true)
            {
                try
                {
                    if ((BotParams.CurTime.CompareTo("09:00:20") >= 0 && BotParams.CurTime.CompareTo("09::01:59") < 0)  && IsLiquidation == true)
                    {
                        IsLiquidation = false;
                        GetAccountInformation();
                    }

                    if (BotParams.CurTime.CompareTo("15:15:00") >= 0 && BotParams.CurTime.CompareTo("15:19:30") < 0)
                    {
                        for (int Idx = 0; Idx < BotParams.RequestRealDataScrNo.Count; Idx++)
                        {
                            API.DisconnectRealData(BotParams.RequestRealDataScrNo[Idx]);
                        }

                        BotParams.IsLiquidation = true;
                        GetAccountInformation();

                        // 정리 매매
                        // LiquidationStocks();
                    }

<<<<<<< HEAD
                    if (BotParams.CurTime.CompareTo("09:02:00") >= 0 && BotParams.CurTime.CompareTo("15:14:59") < 0 && BotParams.PendingOrders.Count() == 0 && batchData == false)
=======
                    if (BotParams.CurTime.CompareTo("09:01:00") >= 0 && BotParams.CurTime.CompareTo("15:14:59") < 0 && BotParams.IsLiquidation == false && batchData == false)
>>>>>>> 79776180eacea51c114a25c945412e8c703242ff
                    {
                        BotParams.comparedTime = DateTime.Parse(BotParams.CurTime);
                        batchData = true;
                        BotParams.IsLiquidation = false;

                        GetAccountInformation();         // 정리매매 때 이미 GetAccountInformation을 불러왔으니까 여기선 안불러와도 되나?
                        GetShortCodes("MM");                // botParams.Codes 에 저장
                        RequestStocksData();                // Request TrData/TrRealData & Update the stockState Dictionary on realtime.
                    }

                    else if (TestCheck.Checked && batchData == false)
                    {
                        batchData = true;
                        BotParams.comparedTime = DateTime.Parse(BotParams.CurTime);
                        BotParams.ArrangingPosition = true;

                        GetAccountInformation();
                        GetShortCodes("MM");            // botParams.Codes 에 저장
                        RequestStocksData();                // Request TrData/TrRealData & Update the stockState Dictionary on realtime.
                    }
                }

                catch(Exception e)
                {
                    logs.write_sys_log(e.ToString(), 0);
                    continue;
                }

            }

        }

        private void TestCheck_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
