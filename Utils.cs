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
                    logs.write_sys_log("delay() ex.Message : [" + ex.Message + "]\r\n", 0);
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

    }
}
