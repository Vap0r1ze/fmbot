using Discord.WebSocket;

namespace FMBot.Bot.Models
{
    public class InvocationContext
    {
        public SocketUser Target { get; set; }

        public string[] CleanArgs { get; set; }
    }
}
