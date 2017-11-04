using System;
using System.Threading.Tasks;
using DSharpPlus;
using NLog;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DSharpPlus.EventArgs;
using System.Configuration;

namespace PatBot
{
    class Program
    {
        static DiscordClient discord;
        static List<ICommand> commands = new List<ICommand>();
        static Logger logger = LogManager.GetCurrentClassLogger();

        static void RegisterCommands()
        {
            commands.Add(new Commands.AssignPronoun());
            commands.Add(new Commands.Ping());
            commands.Add(new Commands.Roll());
            commands.Add(new Commands.SayGoodnight());
            commands.Add(new Commands.UnassignPronoun());
        }

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static bool CommandIsMatch(string content, ICommand cmd)
        {
            foreach (string regex in cmd.MessageCommandAntiMatchRegexes)
            {
                if (Regex.IsMatch(content, regex))
                {
                    return false;
                }
            }
            foreach (string regex in cmd.MessageCommandMatchRegexes)
            {
                if (Regex.IsMatch(content, regex))
                {
                    return true;
                }
            }

            return false;
        }

        static async Task OnMessageReceived(MessageCreateEventArgs args)
        {
            logger.Trace("Message Received.");

            string msg = args.Message.Content.ToLower();
            foreach (var cmd in commands)
            {
                if (CommandIsMatch(msg, cmd))
                {
                    logger.Info("Command matched. Command class name: " + cmd.GetType().Name);
                    await cmd.Do(args.Message, args.Guild);
                    break;
                }
            }
        }

        static async Task MainAsync(string[] args)
        {
            logger.Info("Pat-Bot started.");
            RegisterCommands();

            string token = ConfigurationManager.AppSettings["DiscordBotToken"];

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += OnMessageReceived;

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}