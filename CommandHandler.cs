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
            Minesweeper ms = new Minesweeper();
            ms.GenerateBoard();
            await arg.FollowupAsync(":one:"); //Send reply

            return Task.CompletedTask;
        }
    }
}
