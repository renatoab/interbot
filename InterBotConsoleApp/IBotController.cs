namespace InterBotConsoleApp
{
    public interface IBotController
    {
        void StartBot(object sender, Telegram.Bot.Args.MessageEventArgs e);
    }
}