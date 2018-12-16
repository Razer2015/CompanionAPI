using Discord.Commands;
using Discord.Modules;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Origin.Authentication;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Discord
{
    public class StartupService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly Auth _auth;

        // DiscordSocketClient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public StartupService(
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            Auth auth)
        {
            _config = config;
            _discord = discord;
            _commands = commands;
            _auth = auth;
        }

        public async Task StartAsync()
        {
            string discordToken = _config["tokens:discord"];     // Get the discord token from the config file
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Please enter your bot's token into the '_configuration.json' file found in the applications root directory.");

            await _discord.LoginAsync(TokenType.Bot, discordToken);     // Login to discord
            await _discord.StartAsync();                                // Connect to the websocket


            string email = _config["credentials:email"];
            string password = _config["credentials:password"];
            if (!ConnectCompanion(email, password))
                throw new Exception("Couldn't connect to Companion. Make sure your email and password are correct in the '_configuration.json' file.");

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());     // Load commands and modules into the command service
            await _commands.AddModulesAsync(typeof(StatsModule).Assembly);
            await _commands.AddModulesAsync(typeof(HelpModule).Assembly);
        }

        private bool ConnectCompanion(string email, string password) {
            try {
                // Get authentication token
                _auth.Login(email, password);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }
        }
    }
}
