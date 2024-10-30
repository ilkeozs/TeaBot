using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using TeaBot.Commands.Other;
using TeaBot.Other;

namespace TeaBot.Commands
{
    public class General : BaseCommandModule
    {

        private string dev = "github.com/ilkeozs";

        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder()
            {
                Title = "Command List",
                Description = "**General Commands**\n\n**tb rules**\n``Shows server rules.``\n\n**tb info**\n``Information about the bot and developer.``\n\n**tb card**\n``A fun card game!``\n\n**tb kiss {member}**\n``Kiss your love!``\n\n**tb mu**\n``Random motivational sentences!``\n\n**Moderation Commands**\n\n**tb ban {member, delete message days (min. 0 | max. 7), reason}**\n``To ban members from the server.``\n\n**tb unban {member, reason}**\n``To unban members from the server.``\n\n**tb del**\n``Deletes all messages in the channel. This process can take a very long time!``\n\n**tb clear {number of messages}**\n``Deletes messages in the channel. The number of messages must be between 0 and 254!``",
                ImageUrl = "https://i.pinimg.com/originals/6b/08/8a/6b088a8a5074b4139785aecf5bda3c2e.gif",
                Color = DiscordColor.Red,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Developer: {dev}"
                },
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("rules")]
        public async Task RulesCommand(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder()
            {
                Title = "**Server Rules**",
                Description = "**Advertisement**\r\n- It is forbidden to make verbal advertisements, advertisements with links, advertisements in private, advertisements with pictures and similar forms.\r\n\r\n\r\n**Swearing, Slang, Insult**\r\n- It is forbidden to swear and use slang in every channel.\r\n- Insulting and mocking members is prohibited.\r\n\r\n\r\n**Channels**\r\n- It is forbidden to use commands other than the command channel.\r\n- It is forbidden to open music except for the audio music channel.\r\n- You can find useful information in channel descriptions.\r\n\r\n\r\n**Authorities and Authority**\r\n- Asking for authorization is prohibited.\r\n- It is forbidden to @tag the authorities in vain and to spam by @tagging them.\r\n- Respect the authorities.\r\n\r\n\r\n**Spam and Tagging**\r\n- Spamming is prohibited.\r\n- It is forbidden to write a word in a continuous message.\r\n- It is forbidden to constantly @tag a member.\r\n\r\n\r\n**Religion, Politics, Sexuality**\r\n- It is forbidden to talk and discuss about religion, to put usernames related to religion.\r\n- It is forbidden to talk about politics, to discuss, to put usernames related to politics.\r\n- Sharing and talking 18+ photos is prohibited.\r\n\r\n\r\n**Fight, Arguing**\r\n- Fighting, getting involved in a fight and arguing are prohibited.",
                Color = DiscordColor.SpringGreen
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("info")]
        public async Task InfoCommand(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder()
            {
                Title = "Information",
                Description = $"**Developer: {dev}\nBot Language: .NET (DSharpPlus Library)\nTeaBot Latency: {ctx.Client.Ping} ms**\n\n**tb server**\n``Developer's discord server invite url.``",
                Color = DiscordColor.Yellow
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("server")]
        public async Task ServerUrl(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder()
            {
                Title = "Developer's Discord Server (Turkish)",
                Url = "https://discord.gg/UzqB8SDm6Z",
                Color = DiscordColor.Yellow
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("card")]
        public async Task CardGame(CommandContext ctx)
        {
            var userCard = new CardSystem();

            var userCardEmbed = new DiscordEmbedBuilder()
            {
                Title = $"Your card is {userCard.SelectedCard}",
                Color = DiscordColor.HotPink
            };

            await ctx.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCard = new CardSystem();

            var botCardEmbed = new DiscordEmbedBuilder()
            {
                Title = $"TeaBot drew a {botCard.SelectedCard}",
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.SelectedNumber > botCard.SelectedNumber)
            {
                //User wins
                var winMessage = new DiscordEmbedBuilder()
                {
                    Title = "Congratulations, you win the game!",
                    Color = DiscordColor.Green
                };
                await ctx.Channel.SendMessageAsync(embed: winMessage);
            }
            else if (userCard.SelectedNumber < botCard.SelectedNumber)
            {
                //Bot wins
                var loseMessage = new DiscordEmbedBuilder()
                {
                    Title = "You lost the game!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: loseMessage);
            }
            else
            {
                //Draw
                var drawMessage = new DiscordEmbedBuilder()
                {
                    Title = "The game is a draw!",
                    Color = DiscordColor.Purple
                };
                await ctx.Channel.SendMessageAsync(embed: drawMessage);
            }
        }

        [Command("kiss")]
        public async Task KissCommand(CommandContext ctx, DiscordMember member)
        {

            var kissSystem = new KissSystem();

            if (member == ctx.User)
            {
                var message = new DiscordEmbedBuilder()
                {
                    Title = "Member Error",
                    Description = "You can't kiss yourself! Not like this :)",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message);
            }
            else
            {
                var message = new DiscordEmbedBuilder()
                {
                    Title = "Muah <3",
                    ImageUrl = $"{kissSystem.SelectedLink}",
                    Color = DiscordColor.HotPink,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{ctx.User.Username} kissed {member.Username}",
                    },
                };
                await ctx.Channel.SendMessageAsync(embed: message);
            }
        }

        [Command("mu")]
        public async Task News(CommandContext ctx)
        {
            var motivationSystem = new MotivationSystem();

            var message = new DiscordEmbedBuilder()
            {
                Title = "Motivation Up!",
                Description = $"{motivationSystem.SelectedLink}",
                Color = DiscordColor.Green
            };
            await ctx.Channel.SendMessageAsync(embed: message);
        }

        [Command("hi")]
        public async Task HiCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} Hi! How are you?");
        }

    }
}