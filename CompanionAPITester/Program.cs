using System;
using CompanionAPI;
using Config.Net;

namespace CompanionAPITester
{
    class Program
    {
        static void Main(string[] args) {
            var settings = new ConfigurationBuilder<ICompanionConfig>()
#if DEBUG
                .UseJsonFile("config.dev.json")
#else
                .UseJsonFile("config.json")
#endif
                .Build();
            
            try {
                // Get authentication token
                var auth = new Origin.Authentication.Auth();
                auth.Login(settings.Email, settings.Password);

                // Search persona
                if (auth.SearchPersonaId(settings.PersonaName, out var user, out var status)) {
                    // Login and retrieve session token
                    var companion = new CompanionClient();
                    if (companion.Login(auth.CompanionToken, out var responseStatus)) {
                        // Get detailed stats
                        if (companion.GetDetailedStats(settings.Game, user.PersonaId, out var stats, out responseStatus)) {
                            Console.WriteLine($@"Player: {user.EAID} ({user.PersonaId})
StatsType: {stats.DetailedStatType}
TimePlayed: {stats.BasicStats.TimePlayed}
Wins: {stats.BasicStats.Wins}
Losses: {stats.BasicStats.Losses}
Kills: {stats.BasicStats.Kills}
Deaths: {stats.BasicStats.Deaths}
Kpm: {stats.BasicStats.KPM}
Spm: {stats.BasicStats.SPM}
Skill: {stats.BasicStats.Skill}");
                        }
                        else {
                            Console.WriteLine($"{responseStatus.Status}: Stats retrieval failed - {responseStatus.Message}");
                        }
                    }
                    else {
                        Console.WriteLine($"{responseStatus.Status}: Login failed - {responseStatus.Message}");
                    }
                }
                else {
                    Console.WriteLine($"Error: PersonaId search failed for {settings.PersonaName} because {status}.");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
