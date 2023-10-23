using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

class Program
{
    private DiscordSocketClient _client;

    static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    public async Task MainAsync()
    {
        // Initialize the Discord client
        _client = new DiscordSocketClient();

        // Log events
        _client.Log += LogAsync;
        _client.Ready += ReadyAsync;
        _client.MessageReceived += MessageReceivedAsync;

        string token = "YOUR_BOT_TOKEN";

        // Log in using the bot token
        await _client.LoginAsync(TokenType.Bot, token);

        // Start the bot
        await _client.StartAsync();

        // Delay the program indefinitely to keep the bot running
        await Task.Delay(-1);
    }

    private Task LogAsync(LogMessage log)
    {
        // Log messages to the console
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private Task ReadyAsync()
    {
        // Print a message when the bot is ready
        Console.WriteLine($"Bot is connected as {_client.CurrentUser}");
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        // Check if the message is a user message and not from a bot
        if (message is not IUserMessage userMessage || message.Author.IsBot)
            return;

        // Check if the message content is "!hello"
        if (userMessage.Content.Equals("!hello"))
        {
            // Send a reply to the channel
            await userMessage.Channel.SendMessageAsync("Hello!");
        }
    }
}
