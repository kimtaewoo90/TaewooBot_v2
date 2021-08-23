using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Telegram Using
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaewooBot_v2
{
    public class TelegramClass
    {
        Logs logs = new Logs();
        public string ChatId { get; set; } = "1542664370";

        private static readonly TelegramBotClient Bot = new TelegramBotClient("1474721655:AAH7cSJoNQdesO_lXRRGUf__mGIInPpicdU");

        public TelegramClass()
        {
        }

        public async void SendTelegramMsg(string message)
        {
            await Bot.SendTextMessageAsync(ChatId, message);
        }

    }
}
