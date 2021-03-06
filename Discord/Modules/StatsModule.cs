﻿using System.Threading.Tasks;
using Discord.Commands;
using System;
using Microsoft.Extensions.Configuration;
using Origin.Authentication;
using CompanionAPI;
using Discord.WebSocket;
using CompanionAPI.Models;
using OriginAPI.Models;
using Discord.Models;
using System.Text;
using System.Linq;

namespace Discord.Modules
{
    public class StatsModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        private readonly Auth _auth;
        private readonly CompanionClient _companionClient;
        private readonly PersonaService _personaService;

        public StatsModule(CommandService service,
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

        [Command("adminsetbfpersona")]
        [Alias("asp")]
        [Summary("Set personaId for any user (Admin).")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SavePersona(string playerName, [Remainder]SocketGuildUser socketUser) {
            // Search persona
            if (_auth.SearchPersonaId(playerName, out var user, out var status)) {
                try {
                    var result = _personaService.AddPersona(socketUser.Id, user.UserId, user.PersonaId);
                    switch (result) {
                        case AddStatus.Fail:
                            await ReplyAsync("**Error**: Something went wrong while adding a new persona.");
                            break;
                        case AddStatus.Overwritten:
                        case AddStatus.Success:
                            await ReplyAsync($"**Success**: Persona {user.EAID} added succesfully.");
                            break;
                    }
                }
                catch (Exception e) {
                    await ReplyAsync($"**Error**: {e.Message}");
                }
            }
            else {
                Console.WriteLine($"**Error**: PersonaId search failed for {playerName} because {status}.");
            }
        }

        [Command("setbfpersona")]
        [Alias("setpersona")]
        [Summary("Set personaId for user.")]
        public async Task SavePersona(string playerName) {
            // Search persona
            if (_auth.SearchPersonaId(playerName, out var user, out var status)) {
                try {
                    var result = _personaService.AddPersona(Context.User.Id, user.UserId, user.PersonaId);
                    switch (result) {
                        case AddStatus.Fail:
                            await ReplyAsync("**Error**: Something went wrong while adding a new persona.");
                            break;
                        case AddStatus.Overwritten:
                        case AddStatus.Success:
                            await ReplyAsync($"**Success**: Persona {user.EAID} added succesfully.");
                            break;
                    }
                }
                catch (Exception e) {
                    await ReplyAsync($"**Error**: {e.Message}");
                }
            }
            else {
                Console.WriteLine($"**Error**: PersonaId search failed for {playerName} because {status}.");
            }
        }

        [Command("bf4stats")]
        [Alias("bf4")]
        [Summary("Retrieve stats for BF4 player.")]
        public async Task BF4Stats(string userName) {
            await ProcessCommand(userName, Games.BF4);
        }

        [Command("bf1stats")]
        [Alias("bf1")]
        [Summary("Retrieve stats for BF1 player.")]
        public async Task BF1Stats(string userName) {
            await ProcessCommand(userName, Games.BF1);
        }

        [Command("bfvstats")]
        [Alias("bfv")]
        [Summary("Retrieve stats for BFV player.")]
        public async Task BFVStats(string userName) {
            await ProcessCommand(userName, Games.BFV);
        }

        private async Task ProcessCommand(string userName, Games game) {
            var mention = Context.Message.MentionedUsers.FirstOrDefault();
            if (mention != null) {
                await StatsByGuildUser((SocketGuildUser)mention, game);
            }
            else {
                await StatsByName(userName, game);
            }
        }

        private async Task StatsByName(string userName, Games game) {
            // Search persona
            if (_auth.SearchPersonaId(userName, out var user, out var status)) {
                try {
                    var personaInfo = (CareerViewModel)_companionClient.GetPersonaInfo(user.PersonaId);
                    switch (game) {
                        case Games.BF4:
                            await PrintBF4Stats(user, personaInfo);
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
                    await ReplyAsync($"**Error**: {e.Message}");
                }
            }
            else {
                Console.WriteLine($"Error: PersonaId search failed for {userName} because {status}.");

                await ReplyAsync($"**Error**: Couldn't find or found too many players with the search name. Try searching with the exact playername.");
            }
        }
        private async Task StatsByGuildUser(SocketGuildUser socketUser, Games game) {
            // Check if Guild User has player saved
            if (_personaService.TryGetPersonaId(socketUser.Id, out var persona)) {
                // Search persona
                if (_auth.SearchPersonaId(persona.EAUserId, out var user)) {
                    try {
                        var personaInfo = (CareerViewModel)_companionClient.GetPersonaInfo(user.PersonaId);
                        switch (game) {
                            case Games.BF4:
                                await PrintBF4Stats(user, personaInfo);
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
                        await ReplyAsync($"**Error**: {e.Message}");
                    }
                }
                else {
                    Console.WriteLine($"Error: PersonaId search failed for {socketUser.Username}.");
                }
            }
            else {
                // TODO: Make command dynamic
                await ReplyAsync($"**Error**: Selected user doesn't have a persona set. Trying to search with the playername.");
                await StatsByName(socketUser.Username, game);
            }
        }

        private string GetPlayTime(TimeSpan time) {
            var sb = new StringBuilder();
            sb.Append(":clock4: ");
            sb.Append(time.Days);
            sb.Append("d ");
            sb.Append(time.Hours);
            sb.Append("h ");
            sb.Append(time.Minutes);
            sb.Append("m of Playtime");

            return sb.ToString();
        }
        private EmbedBuilder BuildEmbed(AtomUser user, BasicStats stats, string statsUrl) {
            TimeSpan time = TimeSpan.FromSeconds((ulong)stats.TimePlayed);

            var kdr = ((stats.Kills == 0) ? 0 : ((double)stats.Deaths == 0) ? (double)stats.Kills : Math.Round((double)stats.Kills / (double)stats.Deaths, 2));
            var wins = ((double)stats.Wins == 0 ? 0 : (double)stats.Wins / ((double)stats.Wins + (double)stats.Losses)) * 100;

            string prefix = _config["[BB_PREFIX]"];
            var embed = new EmbedBuilder {
                Author = new EmbedAuthorBuilder() {
                    Name = $"{stats.Rank.Name} - {user.EAID}",
                    IconUrl = stats.Rank.ImageUrl.Replace("[BB_PREFIX]", prefix),
                    Url = statsUrl
                }
            };

            embed.WithTitle(GetPlayTime(time));

            embed.AddField("**SCORE/MIN**", stats.SPM.Value.ToString("0.##"), true);
            embed.AddField("**KILLS/MIN**", stats.KPM.Value.ToString("0.##"), true);
            embed.AddField("**KILLS**", stats.Kills.ToString(), true);
            embed.AddField("**WINS**", stats.Wins.ToString(), true);
            embed.AddField("**DEATHS**", stats.Deaths.ToString(), true);
            embed.AddField("**LOSSES**", stats.Losses.ToString(), true);
            embed.AddField("**K/D RATIO**", kdr.ToString("0.##"), true);
            embed.AddField("**WINS**", $"{wins.ToString("0.##") } %", true);

            var random = new Random();
            embed.Color = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

            embed.Footer = new EmbedFooterBuilder() {
                Text = $"© xfileFIN's Stats Module ({DateTime.Now.Year})",
            };
            embed.WithTimestamp(DateTimeOffset.UtcNow);

            return embed;
        }

        private async Task PrintBF4Stats(AtomUser user, CareerViewModel career) {
            if (career.GameStats.BF4 == null) {
                await ReplyAsync("User doesn't own the game.");

                return;
            }
            var embed = BuildEmbed(user, career.GameStats.BF4, $"http://battlelog.battlefield.com/bf4/soldier/{user.EAID}/stats/{user.PersonaId}/pc/");

            //if (career.Emblem != null) {
            //    embed.WithThumbnailUrl(career.Emblem.Replace("[SIZE]", "256").Replace("[FORMAT]", "png"));
            //}
            embed.WithThumbnailUrl(user.Avatar);
            embed.WithUrl($"https://bf4stats.com/pc/{user.EAID}");

            await ReplyAsync("**Battlefield 4**", embed: embed.Build());
        }
        private async Task PrintBF1Stats(AtomUser user, CareerViewModel career) {
            if (career.GameStats.BF1 == null) {
                await ReplyAsync("User doesn't own the game.");

                return;
            }
            var embed = BuildEmbed(user, career.GameStats.BF1, $"https://www.battlefield.com/companion/career/{user.PersonaId}/bf1");

            //if (career.Emblem != null) {
            //    embed.WithThumbnailUrl(career.Emblem.Replace("[SIZE]", "256").Replace("[FORMAT]", "png"));
            //}
            embed.WithThumbnailUrl(user.Avatar);
            embed.WithUrl($"https://battlefieldtracker.com/bf1/profile/pc/{user.EAID}");

            await ReplyAsync("**Battlefield 1**", embed: embed.Build());
        }
        private async Task PrintBFVStats(AtomUser user, CareerViewModel career) {
            if (career.GameStats.BFV == null) {
                await ReplyAsync("User doesn't own the game.");

                return;
            }
            var embed = BuildEmbed(user, career.GameStats.BFV, $"https://battlefieldtracker.com/bfv/profile/origin/{user.EAID}/overview");

            //if (career.Emblem != null) {
            //    embed.WithThumbnailUrl(career.Emblem.Replace("[SIZE]", "256").Replace("[FORMAT]", "png"));
            //}
            embed.WithThumbnailUrl(user.Avatar);
            embed.WithUrl($"https://battlefieldtracker.com/bfv/profile/origin/{user.EAID}/overview");

            await ReplyAsync("**Battlefield V**", embed: embed.Build());
        }
    }
}