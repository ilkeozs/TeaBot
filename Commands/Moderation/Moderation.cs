using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace TeaBot.Commands.Moderation_Commands
{
    public class Moderation : BaseCommandModule
    {
        [Command("ban")]
        public async Task BanCommand(CommandContext ctx, DiscordMember member, int delete_message_days = 0, string reason = null)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                try
                {
                    await ctx.Guild.BanMemberAsync(member, delete_message_days, reason);

                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Member Banned",
                        Description = $"{member.Username} successfully banned from the server by {ctx.User.Username}\nReason: {reason}\nAll messages from {member.Username} for the {delete_message_days} days ago have been deleted!",
                        Color = DiscordColor.Green
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
                catch (Exception)
                {
                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = "**Possible Reasons**\n\n``The user to be banned from the server must have a lower permission than the bot.\n\nThe delete message days value was entered outside the specified values (min. 0 | max. 7).``",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
            }

            else
            {
                var message = new DiscordEmbedBuilder()
                {
                    Title = "Permission Error",
                    Description = "To use this command, you must have permission to ban members!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message);
            }
        }

        [Command("unban")]
        public async Task UnbanCommand(CommandContext ctx, DiscordUser user, string reason = null)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                try
                {
                    await ctx.Guild.UnbanMemberAsync(user);

                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Member Unbanned",
                        Description = $"{user.Username} successfully unbanned from the server by {ctx.User.Username}\nReason: {reason}",
                        Color = DiscordColor.Green
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
                catch (Exception)
                {
                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = "This member is not banned from the server!",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
            }

            else
            {
                var message = new DiscordEmbedBuilder()
                {
                    Title = "Permission Error",
                    Description = "To use this command, you must have permission to ban members!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message);
            }
        }

        [Command("kick")]
        public async Task KickCommand(CommandContext ctx, DiscordMember member, string reason = null)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.KickMembers))
            {
                try
                {
                    await member.RemoveAsync(reason);

                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Member Kicked",
                        Description = $"{member.Username} successfully kicked from the server by {ctx.User.Username}\nReason: {reason}",
                        Color = DiscordColor.Green
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
                catch (Exception)
                {
                    var message = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = "**Possible Reasons**\n\n``The user to be kicked from the server must have a lower permission than the bot.``",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: message);
                }
            }

            else
            {
                var message = new DiscordEmbedBuilder()
                {
                    Title = "Permission Error",
                    Description = "To use this command, you must have permission to kick members!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message);
            }
        }

        [Command("del")]
        public async Task DeleteCommand(CommandContext ctx)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.ManageMessages))
            {
                var message1 = new DiscordEmbedBuilder()
                {
                    Title = "Warning",
                    Description = "The process is starting!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message1);

                try
                {
                    foreach (var discordMessage in ctx.Channel.GetMessagesAsync().Result)
                    {
                        var deleteMessageTask = discordMessage.DeleteAsync();
                        deleteMessageTask.Wait();
                    }

                    var message2 = new DiscordEmbedBuilder()
                    {
                        Title = "Information",
                        Description = "All messages have been deleted!",
                        Color = DiscordColor.Yellow
                    };
                    await ctx.Channel.SendMessageAsync(embed: message2);
                }

                catch (Exception e)
                {
                    var message3 = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = $"{e.Message}",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: message3);
                }
            }

            else
            {
                var message4 = new DiscordEmbedBuilder()
                {
                    Title = "Permission Error",
                    Description = "To use this command, you must have permission to manage messages!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message4);
            }
        }

        [Command("clear")]
        public async Task ClearCommand(CommandContext ctx, byte number)
        {
            if (ctx.Member.Permissions.HasPermission(Permissions.ManageMessages))
            {
                var message1 = new DiscordEmbedBuilder()
                {
                    Title = "Warning",
                    Description = "The process is starting!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message1);

                try
                {
                    foreach (var discordMessage in ctx.Channel.GetMessagesAsync(number + 2).Result)
                    {
                        var deleteMessageTask = discordMessage.DeleteAsync();
                        deleteMessageTask.Wait();
                    }

                    var message2 = new DiscordEmbedBuilder()
                    {
                        Title = "Information",
                        Description = $"{number} messages have been deleted!",
                        Color = DiscordColor.Yellow
                    };
                    await ctx.Channel.SendMessageAsync(embed: message2);
                }

                catch (Exception e)
                {
                    var message3 = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = $"{e.Message}",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: message3);
                }
            }

            else
            {
                var message4 = new DiscordEmbedBuilder()
                {
                    Title = "Permission Error",
                    Description = "To use this command, you must have permission to manage messages!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: message4);
            }
        }
    }
}