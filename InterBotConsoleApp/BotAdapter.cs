using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InterBotConsoleApp
{
    public class BotAdapter : IBotAdapter
    {
        TelegramBotClient bot;

        public BotAdapter(TelegramBotClient bot)
        {
            this.bot = bot;
        }

        public void EnviarMensagem(long id, string mensagem)
        {
            bot.SendTextMessageAsync(id, mensagem);
        }
    }
}
