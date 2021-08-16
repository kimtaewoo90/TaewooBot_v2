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
    public partial class Logs : Form
    {

        public Logs()
        {
            InitializeComponent();
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
            cur_tm = BotParams.CurTime;

            cur_dtm = "[" + cur_dt + " " + cur_tm + "]";

            if (is_Clear == 1)
            {
                if (LogBox.InvokeRequired)
                {
                    LogBox.BeginInvoke(new Action(() => LogBox.Clear()));
                }
                else
                {
                    this.LogBox.Clear();
                }
            }

            else
            {
                if (this.LogBox.InvokeRequired)
                {
                    LogBox.BeginInvoke(new Action(() => LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine)));
                }

                else
                {
                    this.LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine);
                    // log 기록

                    //File.AppendAllText(BotParams.Path + BotParams.LogFileName, cur_dtm + text + Environment.NewLine, Encoding.Default);
                }
            }
        }

        public void MakeLogFile()
        {

            FileInfo fileInfo = new FileInfo(BotParams.Path + BotParams.LogFileName);

            if (fileInfo.Exists)
            { }
            else
            {
                File.WriteAllText(BotParams.Path + BotParams.LogFileName, BotParams.date + "의 로그기록을 시작합니다.\n", Encoding.Default);
                write_sys_log("로그파일 생성 완료", 0);
            }

        }

        public void MakeTickDataFile()
        {
            FileInfo fileInfo = new FileInfo(BotParams.TickPath + BotParams.TickLogFileName);

            if (fileInfo.Exists)
            { }
            else
            {
                File.WriteAllText(BotParams.TickPath + BotParams.TickLogFileName, BotParams.date + "틱데이터의 기록을 시작합니다\n.", Encoding.Default);
                write_sys_log("틱데이터파일 생성 완료", 0);
            }
        }
    }
}
