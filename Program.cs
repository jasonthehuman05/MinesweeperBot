using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System;
using System.IO;

namespace MinesweeperBot
{
    internal class Program
    {
        public static DiscordSocketClient client;
        static string token = File.ReadAllText("token.txt");

        static CommandHandler commandHandler;

        static void Main(string[] args)
        {
            Console.WriteLine("Minesweeper Bot");
            Console.WriteLine("Starting Bot...");

            //generate command handler
            commandHandler = new CommandHandler();
            MainAsyncProcess();
            while (true) ;
        }

        static async void MainAsyncProcess()
        {
            //Create the discord client and wire up all needed events
            client = new DiscordSocketClient();
            client.Log += DiscordLog;
            client.Ready += BotReady;
            
            //Create event to process command
            client.SlashCommandExecuted += commandHandler.CommandExecuted;

            //Attempt first log in
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            //Do nothing until program is closed
            await Task.Delay(-1);
            await client.StopAsync();
            client.Dispose();
        }

        private static async Task BotReady() //When bot is ready, run the command loader
        {
            Console.WriteLine("Bot Ready!");

            SlashCommandProperties scp = await commandHandler.LoadCommands();
            await client.CreateGlobalApplicationCommandAsync(scp);

            Console.WriteLine("Command Registered!");
        }

        private static Task DiscordLog(LogMessage arg) //Log any messages from the discord gateway
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }
    }
}