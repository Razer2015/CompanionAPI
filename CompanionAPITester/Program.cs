using System;
using CompanionAPI;
using Config.Net;

namespace CompanionAPITester
{
    class Program
    {
        static void Main(string[] args) {
            var settings = new ConfigurationBuilder<ICompanionConfig>()
                .UseJsonFile("config.json")
                .Build();
            
            try {
                // Get authentication token
                var auth = new Authentication.Auth();
                auth.Login(settings.Email, settings.Password, Authentication.AccessType.Companion);

                // Login and retrieve session token
                var companion = new CompanionClient();
                companion.Login(auth.Code);

                // Get detailed stats
                var stats = companion.GetDetailedStats(settings.Game, settings.PersonaId);
                Console.WriteLine($@"StatsType: {stats.DetailedStatType}
TimePlayed: {stats.BasicStats.TimePlayed}
Wins: {stats.BasicStats.Wins}
Losses: {stats.BasicStats.Losses}
Kills: {stats.BasicStats.Kills}
Deaths: {stats.BasicStats.Deaths}
Kpm: {stats.BasicStats.KPM}
Spm: {stats.BasicStats.SPM}
Skill: {stats.BasicStats.Skill}");
            }
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
