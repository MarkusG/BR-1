using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BR_1
{
    class Program
    {
        static void Main(string[] args) => new Program().Start(args); // We use this to get out of the static context

        public static DiscordClient _client;
        public static Server _planetside;

        public void Start(string[] args)
        {
            Console.WriteLine("Enter the bot's token:");
            string token = Console.ReadLine();
            Steam.GetAPIKey();
            Console.WriteLine();
            Steam.InitializeNameDict();
            Console.WriteLine();

            _client = new DiscordClient(x => // Create the _client object and set its values
            {
                x.AppName = "BR-1";
                x.AppUrl = "";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _client.UsingCommands(x => // Use Discord.NET's command engine
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            // Call methods to create everything
            Commands.CreateCommands();
            EventListeners.CreateEventListeners();

            Console.Title = "BR-1";
            // Log into the bot
            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect(token);
            });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[{DateTime.Now}] ");
            switch (e.Severity)
            {
                case LogSeverity.Warning: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case LogSeverity.Error: Console.ForegroundColor = ConsoleColor.Red; break;
                case LogSeverity.Debug: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case LogSeverity.Info: Console.ForegroundColor = ConsoleColor.Green; break;
                case LogSeverity.Verbose: Console.ForegroundColor = ConsoleColor.Magenta; break;
            }
            Console.Write($"[{e.Severity}] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"[{e.Source}] {e.Message}");
        }

    }
}
