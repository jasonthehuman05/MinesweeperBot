using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    internal class CommandHandler
    {
        public async Task<SlashCommandProperties> LoadCommands()
        {
            //Creates the command and assigns the informaiton needed
            SlashCommandBuilder commandBuilder = new SlashCommandBuilder();

            commandBuilder.WithName("minesweeper");
            commandBuilder.WithDescription("Generate a minesweeper board");
            commandBuilder.AddOption("width", ApplicationCommandOptionType.Integer, "Width of the board", isRequired: true);
            commandBuilder.AddOption("height", ApplicationCommandOptionType.Integer, "Height of the board", isRequired: true);

            //Builds it to be added
            SlashCommandProperties scp = commandBuilder.Build();

            return scp;
        }

        public async Task<Task> CommandExecuted(SocketSlashCommand arg)
        {
            await DoAction(arg);

            return Task.CompletedTask;
        }

        public async Task<Task> DoAction(SocketSlashCommand arg)
        {
            arg.DeferAsync(); //Tell discord the command has been received

            //int value = (int)arg.Data.Options.First().Value;
            SocketSlashCommandDataOption[] value = arg.Data.Options.ToArray();
            long width = (long)value[0].Value;
            long height = (long)value[1].Value;
            //Console.WriteLine($"WIDTH = {width}    HEIGHT = {height}");

            Minesweeper ms = new Minesweeper(width, height);
            List<string> board = ms.GenerateBoard();
            await arg.FollowupAsync("Your board:"); //Send reply

            //Send the board
            IMessageChannel channel = arg.Channel;
            
            foreach(string line in board)
            {
                await channel.SendMessageAsync(line);
            }

            return Task.CompletedTask;
        }
    }
}
