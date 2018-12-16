using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Origin.Authentication;
using CompanionAPI;
using Discord.WebSocket;
using CompanionAPI.Models;
using OriginAPI.Models;
using Discord.Models;

namespace Discord.Modules
{
    public class DetailedStatsModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        private readonly Auth _auth;
        private readonly CompanionClient _companionClient;
        private readonly PersonaService _personaService;

        public DetailedStatsModule(CommandService service,
            IConfigurationRoot config,
            Auth auth,
            CompanionClient companionClient,
            PersonaService personaService) {
            _service = service;
            _config = config;
            _auth = auth;
            _companionClient = companionClient;
            _personaService = personaService;
        }
        /*
        [Command("bf1detailed")]
        [Alias("bf1d")]
        [Summary("Retrieve detailed stats for BF1 player.")]
        public async Task BF1Stats(string userName) {
            await StatsByName(userName, Games.BF1);
        }

        [Command("bf1detailed")]
        [Alias("bf1d")]
        [Summary("Retrieve detailed stats for BF1 player.")]
        public async Task BF1Stats([Remainder]SocketGuildUser socketUser) {
            await StatsByGuildUser(socketUser, Games.BF1);
        }

        [Command("bfvdetailed")]
        [Alias("bfvd")]
        [Summary("Retrieve detailed stats for BFV player.")]
        public async Task BFVStats(string userName) {
            await StatsByName(userName, Games.BFV);
        }

        [Command("bfvdetailed")]
        [Alias("bfvd")]
        [Summary("Retrieve detailed stats for BFV player.")]
        public async Task BFVStats([Remainder]SocketGuildUser socketUser) {
            await StatsByGuildUser(socketUser, Games.BFV);
        }

        private async Task StatsByName(string userName, Games game) {
            // Search persona
            if (_auth.SearchPersonaId(userName, out var user, out var status)) {
                try {
                    var personaInfo = _companionClient.GetPersonaInfo(_auth.CompanionToken, user.PersonaId, game.GetCodeName(), "pc");
                    switch (game) {
                        case Games.BF4:
                            // TODO: Print BF4 Stats
                            break;
                        case Games.BF1:
                            await PrintBF1Stats(user, personaInfo);
                            break;
                        case Games.BFV:
                            await PrintBFVStats(user, personaInfo);
                            break;
                    }
                }
                catch (Exception e) {
                    await ReplyAsync($"Error: {e.Message}");
                }
            }
            else {
                Console.WriteLine($"Error: PersonaId search failed for {userName} because {status}.");
            }
        }
        private async Task StatsByGuildUser(SocketGuildUser socketUser, Games game) {
            // Check if Guild User has player saved
            if (_personaService.TryGetPersonaId(socketUser.Id, out var persona)) {
                // Search persona
                if (_auth.SearchPersonaId(persona.EAUserId, out var user)) {
                    try {
                        var personaInfo = _companionClient.GetPersonaInfo(_auth.CompanionToken, user.PersonaId, game.GetCodeName(), "pc");
                        switch (game) {
                            case Games.BF4:
                                // TODO: Print BF4 Stats
                                break;
                            case Games.BF1:
                                await PrintBF1Stats(user, personaInfo);
                                break;
                            case Games.BFV:
                                await PrintBFVStats(user, personaInfo);
                                break;
                        }
                    }
                    catch (Exception e) {
                        await ReplyAsync($"Error: {e.Message}");
                    }
                }
                else {
                    Console.WriteLine($"Error: PersonaId search failed for {socketUser.Username}.");
                }
            }
            else {
                // TODO: Make command dynamic
                await ReplyAsync($"**Error**: Selected user doesn't have a persona set. Try with ?bfvstats <soldierName>");
            }
        }

        private async Task PrintBF1Stats(AtomUser user, PersonaViewModel personaInfo) {
            string prefix = _config["[BB_PREFIX]"];
            var embed = new EmbedBuilder {
                Author = new EmbedAuthorBuilder() {
                    Name = $"{personaInfo.DetailedStats.BasicStats.Rank.Name} - {user.EAID}",
                    IconUrl = personaInfo.DetailedStats.BasicStats.SoldierImageUrl //Context.User.GetAvatarUrl()
                }
            };

            embed.WithThumbnailUrl(personaInfo.EmblemUrl);

            embed.AddField("**Kills**", personaInfo.DetailedStats.BasicStats.Kills.ToString(), true);
            embed.AddField("**Deaths**", personaInfo.DetailedStats.BasicStats.Deaths.ToString(), true);
            embed.AddField("**K/D Ratio**", personaInfo.DetailedStats.KDR.Value.ToString("0.##"), true);

            var random = new Random();
            embed.Color = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

            embed.Footer = new EmbedFooterBuilder() {
                Text = $"© xfileFIN's Stats Module ({DateTime.Now.Year})",
            };

            embed.WithTimestamp(DateTimeOffset.UtcNow);
            await ReplyAsync("**Battlefield 1**", embed: embed.Build());
        }
        private async Task PrintBFVStats(AtomUser user, PersonaViewModel personaInfo) {
            string prefix = _config["[BB_PREFIX]"];
            var embed = new EmbedBuilder {
                Author = new EmbedAuthorBuilder() {
                    Name = $"{personaInfo.DetailedStats.BasicStats.Rank.Name} - {user.EAID}",
                    IconUrl = personaInfo.DetailedStats.BasicStats.Rank.ImageUrl.Replace("[BB_PREFIX]", prefix)
                }
            };

            embed.WithThumbnailUrl(user.Avatar);

            embed.AddField("**Kills**", personaInfo.DetailedStats.BasicStats.Kills.ToString(), true);
            embed.AddField("**Deaths**", personaInfo.DetailedStats.BasicStats.Deaths.ToString(), true);
            embed.AddField("**K/D Ratio**", personaInfo.DetailedStats.KDR.Value.ToString("0.##"), true);
            embed.AddField("**SPM**", personaInfo.DetailedStats.BasicStats.SPM.Value.ToString("0.##"), true);

            var random = new Random();
            embed.Color = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

            embed.Footer = new EmbedFooterBuilder() {
                Text = $"© xfileFIN's Stats Module ({DateTime.Now.Year})",
            };

            embed.WithTimestamp(DateTimeOffset.UtcNow);
            await ReplyAsync("**Battlefield V**", embed: embed.Build());
        }*/
    }
}