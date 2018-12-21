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
                    var companion = new CompanionClient(auth);
                    if (companion.Login(out var responseStatus)) {
                        // Get detailed stats
                        if (companion.GetDetailedStats(settings.Game, user.PersonaId, out var output)) {
                            Console.WriteLine($@"Player: {user.EAID} ({user.PersonaId})
StatsType: {output.Model.DetailedStatType}
TimePlayed: {output.Model.BasicStats.TimePlayed}
Wins: {output.Model.BasicStats.Wins}
Losses: {output.Model.BasicStats.Losses}
Kills: {output.Model.BasicStats.Kills}
Deaths: {output.Model.BasicStats.Deaths}
Kpm: {output.Model.BasicStats.KPM}
Spm: {output.Model.BasicStats.SPM}
Skill: {output.Model.BasicStats.Skill}");
                        }
                        else {
                            Console.WriteLine($"{output.Response.Status}: Stats retrieval failed - {output.Response.Message}");
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
