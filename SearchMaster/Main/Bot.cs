using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SearchMaster
{
    public class Bot
    {
        public Config conf;
        internal DiscordSocketClient discord;
        internal CommandService commands;
        internal IServiceProvider services;

        //conf = JsonSerializer.Deserialize<Config>(File.ReadAllText("../../../Config.json"));
        public async Task MainAsync()
        {
            conf = JsonSerializer.Deserialize<Config>(File.ReadAllText("../../../Config.json"));
            discord = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info
            });
            discord.Log += Log;
            commands = new CommandService();
            services = new ServiceCollection()
                .AddSingleton(discord)
                .AddSingleton(commands)
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();
            await discord.LoginAsync(TokenType.Bot, conf.Token);
            await discord.StartAsync();
            await Task.Delay(Timeout.Infinite);
            discord.Ready += () =>
            {
                Console.WriteLine("Ready uwu");
                return Task.CompletedTask;
            };
            Task Log(LogMessage message)
            {
                Console.WriteLine(message.ToString());
                return Task.CompletedTask;
            }
        }
    }

    internal sealed class CommandHandler
    {
        internal Config conf;
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        public CommandHandler(DiscordSocketClient _client, CommandService _commands)
        {
            commands = _commands;
            client = _client;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            conf = JsonSerializer.Deserialize<Config>(File.ReadAllText("../../../Config.json"));
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;
            if (!(message.HasStringPrefix(conf.Prefix, ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            if (message.Author.IsBot) return;
            var context = new SocketCommandContext(client, message);
            var result = await commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}