using Discord;
using System;
using System.Threading.Tasks;

namespace DiscordTester
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
