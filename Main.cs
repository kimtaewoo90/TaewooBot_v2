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
        // Thread 선언
        Thread GetTime = null;
        Thread GetDataThread = null;
        Thread MonitoringSignalThread = null;
        //Thread OrderThread = null;

        // Instance Other WinForms
        Logs logs = new Logs();
        Params Params = new Params();
        Universe universe = new Universe();
        Position position = new Position();
        Blotter blt = new Blotter();
        Utils utils = new Utils();
        BotParams botParams = new BotParams();

        Dictionary<string, StockState> stockState = new Dictionary<string, StockState>();

        private DateTime compareTime { get; set; }


        delegate void Ctr_Involk(Control ctr, string text);


        public Main()
        {
            InitializeComponent();
      

            // Open Windows
            logs.StartPosition = FormStartPosition.Manual;
            logs.Location = new Point(755, 520);
            logs.Show();

            universe.StartPosition = FormStartPosition.Manual;
            universe.Location = new Point(100, 520);
            universe.Show();

            blt.StartPosition = FormStartPosition.Manual;
            blt.Location = new Point(860, 100);
            blt.Show();

            position.StartPosition = FormStartPosition.Manual;
            position.Location = new Point(100, 100);
            position.Show();

            // Initialize Parameters
            utils.InitialParams();

            // Make Log text files
            logs.MakeLogFile();
            logs.MakeTickDataFile();

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
                botParams.Market = MarketType.Text;
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

            botParams.UserID = "";
            botParams.UserID = API.GetLoginInfo("USER_ID").Trim(); // 사용자 아이디를 가져와서 클래스 변수(전역변수)에 저장
            //textBox1.Text = g_user_id; // 전역변수에 저장한 아이디를 텍스트박스에 출력

            accno_cnt = "";
            accno_cnt = API.GetLoginInfo("ACCOUNT_CNT").Trim(); // 사용자의 증권계좌 수를 가져옴

            // TODO : Error
            accno_arr = new string[int.Parse(accno_cnt)];

            botParams.AccountNumber = API.GetLoginInfo("ACCNO").Trim().Replace(";", "");

            accno_arr = botParams.AccountNumber.Split(';');  // API에서 ';'를 구분자로 여러개의 계좌번호를 던져준다.
            logs.write_sys_log("Account Number : " + botParams.AccountNumber, 0);
            logs.write_sys_log("Welcome " + botParams.UserID, 0);


        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {

            if (botParams.Market == "Stock")
            {
                logs.write_sys_log(botParams.Market, 0);
                Login();


                // 자동매매 Thread 시작
                if (botParams.IsThread is true) // 스레드가 이미 생성된 상태라면
                {
                    logs.write_sys_log("AUTO TRADING SYSTEM is on already. \n", 0);
                    return;
                }

                logs.write_sys_log("AUTO TRADING SYSTEM is just started \r\n", 0);
                botParams.IsThread = true;
                GetDataThread = new Thread(new ThreadStart(GetData));
                MonitoringSignalThread = new Thread(new ThreadStart(MonitoringSignal));
                GetTime = new Thread(new ThreadStart(GetCurrentTime));

                GetTime.Start();
                GetDataThread.Start();
                MonitoringSignalThread.Start();
            }
        }


        // GetTime Thread
        public void GetCurrentTime()
        {
            while (true)
            {
                botParams.CurTime = utils.get_cur_tm();
                // Display time
                CurrentTime.Text = botParams.CurTime;
            }
        }


        // Thread1 (GetData Thread)
        public void GetData()
        {
            TelegramClass telegram = new TelegramClass();

            telegram.SendTelegramMsg("GetData Thread is started");

            var batchData = false;
            // while 문으로 무한루프 & 시간계산
            while(true)
            {
                try
                {
                    if (botParams.CurTime.CompareTo("08:30:01") >= 0 && botParams.CurTime.CompareTo("15:19:59") < 0 && batchData == false)
                    {
                        compareTime = DateTime.Parse(botParams.CurTime);
                        batchData = true;

                        GetAccountInformation();
                        GetShortCodes("Kosdaq");            // botParams.Codes 에 저장
                        RequestStocksData();                // Request TrData/TrRealData & Update the stockState Dictionary on realtime.
                    }

                    else if (TestCheck.Checked && batchData == false)
                    {
                        batchData = true;
                        compareTime = DateTime.Parse(botParams.CurTime);

                        GetAccountInformation();
                        GetShortCodes("Kosdaq");            // botParams.Codes 에 저장
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

        // Thread2 (Monitoring Thread)
        public void MonitoringSignal()
        {
            // TODO : 여기에 계좌정보 계속 업데이트 하면서 Sell Signal 일 때 매도 주문 전송.
            // PositionState에서 정의된 State Dictionary를 무한루프로 계속 모니터링.
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
