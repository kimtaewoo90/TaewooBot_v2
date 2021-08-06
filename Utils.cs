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

using System.IO;


namespace TaewooBot_v2
{
    public partial class Utils
    {
        Logs logs = new Logs();
        Universe universe = new Universe();
        BotParams botParams = new BotParams();

        // Utils
        //  현재시간 불러오기
        public string get_cur_tm()
        {
            DateTime cur_time;
            string cur_tm;

            cur_time = DateTime.Now;
            cur_tm = cur_time.ToString("HH:mm:ss");

            return cur_tm;
        }

        // 시스템 로그 함수 구현
        public void write_sys_log(String text, int is_Clear)
        {
            DateTime cur_time;
            String cur_dt;
            String cur_tm;
            String cur_dtm;

            cur_dt = "";
            cur_tm = "";

            cur_time = DateTime.Now;
            cur_dt = cur_time.ToString("yyyy-") + cur_time.ToString("MM-") + cur_time.ToString("dd");
            cur_tm = get_cur_tm();

            cur_dtm = "[" + cur_dt + " " + cur_tm + "]";

            if (is_Clear == 1)
            {
                if (logs.LogBox.InvokeRequired)
                {
                    logs.LogBox.BeginInvoke(new Action(() => logs.LogBox.Clear()));
                }
                else
                {
                    this.logs.LogBox.Clear();
                }
            }

            else
            {
                if (this.logs.LogBox.InvokeRequired)
                {
                    logs.LogBox.BeginInvoke(new Action(() => logs.LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine)));
                }

                else
                {
                    this.logs.LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine);
                    // log 기록

                    File.AppendAllText(botParams.Path + botParams.LogFileName, cur_dtm + text + Environment.NewLine, Encoding.Default);
                }
            }
        }

        public void MakeLogFile()
        {

            FileInfo fileInfo = new FileInfo(botParams.Path + botParams.LogFileName);

            if (fileInfo.Exists)
            {}
            else
            {
                File.WriteAllText(botParams.Path + botParams.LogFileName, botParams.date + "의 로그기록을 시작합니다.\n", Encoding.Default);
                write_sys_log("로그파일 생성 완료", 0);
            }

        }

        public void MakeTickDataFile()
        {
            FileInfo fileInfo = new FileInfo(botParams.TickPath + botParams.TickLogFileName);

            if (fileInfo.Exists)
            { }
            else
            {
                File.WriteAllText(botParams.TickPath + botParams.TickLogFileName, botParams.date + "틱데이터의 기록을 시작합니다\n.", Encoding.Default);
                write_sys_log("틱데이터파일 생성 완료", 0);
            }
        }


        // 지연함수 구현
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public DateTime delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                try
                {
                    unsafe
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                catch (AccessViolationException ex)
                {
                    write_sys_log("delay() ex.Message : [" + ex.Message + "]\r\n", 0);
                }

                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        // 요청번호 부여 함수 구현
        public string get_scr_no()
        {
            if (botParams.ScrNo < 9999)
            {
                botParams.ScrNo++;
            }
            else botParams.ScrNo = 1000;

            return botParams.ScrNo.ToString();
        }

        // Params 초기화
        public void InitialParams()
        {
            botParams.IsThread = false;
            botParams._SearchCondition = false;
            botParams._GetTrData = false;
            botParams._GetRTD = false;
            botParams.LossCut = 0.03;
            botParams.ScrNo = 1000;

        }

        public void RemoveDict(string StockCode)
        {

            botParams.targetDict.Remove(StockCode);
            botParams.StockKrNameDict.Remove(StockCode);
            botParams.StockPriceDict.Remove(StockCode);
            botParams.TickSpeedDict.Remove(StockCode);
            botParams.StockPnLDict.Remove(StockCode);


        }

      

        public void DisplayTargetStocks(string Type, string StockCode, string StockName, string Price, string Change, string TickSpeed)
        {
            if (Type == "Insert")
            {
                int AddCnt = universe.TargetStocks.Rows.Count;
                // StockCode, StockKrName, Price, TickSpeed, PnL

                universe.TargetStocks.Rows.Add(StockCode, StockName, Price, TickSpeed, Change);

                
                write_sys_log(StockCode + "종목이 추가 되었습니다.", 0);
            }

            else if (Type == "Update")
            {
                int UpdateCnt = universe.TargetStocks.Rows.Count;
                for (int i = 0; i < UpdateCnt-1; i++)
                {
                    if (universe.TargetStocks["StockCode", i].Value.ToString() == StockCode)
                    {
                        universe.TargetStocks.Rows[i].Cells[2].Value = Price;
                        universe.TargetStocks.Rows[i].Cells[3].Value = Change;
                        universe.TargetStocks.Rows[i].Cells[4].Value = TickSpeed;

                    }
                }
            }
        }

        public void DeleteTargetStocks(string StockCode)
        {
            try
            {
                int DelCnt = universe.TargetStocks.Rows.Count;
                for (int i = 0; i < DelCnt; i++)
                {
                    try
                    {
                        if (universe.TargetStocks["StockCode", i].Value.ToString() == StockCode && universe.TargetStocks != null)
                        {
                            // 해당 데이터 삭제
                            universe.TargetStocks.Rows.Remove(universe.TargetStocks.Rows[i]);

                            write_sys_log(StockCode + "종목이 삭제 되었습니다.", 0);
                            break;

                        }
                    }
                    catch(Exception err)
                    {
                        write_sys_log(err.ToString(), 0);
                    }

                }
            }

            catch (Exception err)
            {
                write_sys_log(err.ToString(), 0);
            }

        }
    }
}
