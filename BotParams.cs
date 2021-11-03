using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    public static class BotParams
    {
        public static string version { get; set; } = "2.0.010";
        
        // Initialize global params
        public static string Market { get; set; }
        public static string UserID { get; set; }
        public static int ScrNo { get; set; }
        public static bool IsThread { get; set; }
        public static bool CoinThread { get; set; }
        public static bool _SearchCondition { get; set; }
        public static bool _GetTrData { get; set; }
        public static bool _GetRTD { get; set; }
        public static string RqName { get; set; }
        public static string CurTime { get; set; }
        public static bool IsLiquidation { get; set; } = false;

        public static bool Test { get; set; } = false;

        // About Account Status
        public static string AccountNumber { get; set; } = null;
        public static double Deposit { get; set; } = 0.0;
        public static double todayChange { get; set; } = 0.0;
        public static double todayPnL { get; set; } = 0.0;
        public static double positionPnL { get; set; } = 0.0;

        public static int AccountStockLots { get; set; }
        public static int TotalPnL { get; set; }

        // Slow Params
        public static double LossCut { get; set; }
        public static DateTime comparedTime { get; set; }


        // Logs File
        public static string date { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        public static string Path { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/LogFile/";
        public static string TickPath { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/LogFile/TickLog/";
        public static string LogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_Log.txt";
        public static string TickLogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_TickLog.txt";

        // Stocks
        public static List<string> TargetCodes = new List<string>();
        public static List<string> RequestRealDataScrNo = new List<string>();

        // Market
        public static string[] Codes;
        public static string Kospi { get; set; } = null;
        public static string Kosdaq { get; set; } = null;
        public static string AllMarket { get; set; } = null;

        // Thread
        public static List<string> AccountList = new List<string>();                                                            // Account 상황 실시간 Display

        // Tick Speed Dictionary.
        public static Dictionary<string, List<double>> TickList = new Dictionary<string, List<double>>();                       // 장시작부터 방금전까지의 Tick 리스트
        public static Dictionary<string, List<double>> TickOneMinsList = new Dictionary<string, List<double>>();                // 1분동안의 Tick 리스트
        public static Dictionary<string, double> BeforeAvg = new Dictionary<string, double>();                                  // 장시작부터 방금전까지의 Tick 평균

        public static int StockCnt { get; set; } = 0;                                                                           // Universe Stock Count

        public static Dictionary<string, StockState> stockState = new Dictionary<string, StockState>();

        public static Dictionary<string, PositionState> Accnt_Position = new Dictionary<string, PositionState>();               // 보유 종목 기본 정보 Dictionary

        // Blotter List
        public static List<string> BltList = new List<string>();                                                                // Blotter Display 리스트
        public static List<string> OrderedStocks = new List<string>();                                                          // 주문 체결 종목
        public static List<string> OrderingStocks = new List<string>();                                                         // 주문 접수 종목
        
        public static Dictionary<List<string>, string> OrderNumberAndOrderType = new Dictionary<List<string>, string>();        // Blotter 중복 방지
        public static Dictionary<string, BlotterState> BlotterStateDict = new Dictionary<string, BlotterState>();               // Display Blotter
        public static Dictionary<string, double> LowPriceOneMinute = new Dictionary<string, double>();                          // 1분봉 중 저가 계산 시 (calculateTickSpeed Func)

        // 익절/손절 횟수
        public static int profitTimes { get; set; } = 0;                                                                        // 익절 갯수
        public static int losscutTimes { get; set; } = 0;                                                                       // 손절 갯수

        // Order
        public static string OrderType { get; set; }                                                                            // 장전, 장후 ( 이건 모의투자에서 제공 x)
        public static Dictionary<string, bool> SellSignals = new Dictionary<string, bool>();                                    // 매수 시그널
        public static List<string> PendingOrders = new List<string>();                                                          // Pending 된 주문
        public static List<string> BuyList = new List<string>();                                                                // 매수주문이 이미 들어간 종목 리스트
        public static List<string> SellList = new List<string>();                                                               // 매도주문이 이미 들어간 종목 리스트
        public static Dictionary<string, double> TradedPnL = new Dictionary<string, double>();                                  // 매도 후 손익

    }
}
