using System;
using System.Threading.Tasks;
using Discord.Webhook;

namespace TestingApp;

public static class Program
{
	public static async Task Main(string[] args)
	{
		Console.WriteLine("Enter a Discord Webhook URL to test with:");
		var url = Console.ReadLine();

		if (url is null)
		{
			Console.WriteLine("Set an actual URL, dummy");
			return;
		}

		var webobj = new WebhookObject();

		webobj.AddEmbed(builder =>
		{
			builder.WithTitle("Discord-Webhook lib")
				.WithDescription("Building embed with 'AddEmbed(Action<EmbedBuilder> embedBuilderFunction)'")
				.WithUrl("https://github.com/ToshiroZ/Discord-Webhook")
				.WithThumbnail("https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg")
				.WithColor(Colors.Magenta)
				.WithImage("https://i.imgur.com/ZGPxFN2.jpg")
				.AddField("New Field", "This is a new field")
				.WithFooter("DiscordUser",
					"https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg");
		});

		webobj.AddEmbed(new EmbedBuilder()
			.WithTitle("Discord-Webhook lib second message")
			.WithDescription("Testing discord Webhook")
			.WithColor(Colors.Orange)
		);

		await new Webhook(url, "Webhook library test", "https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg").SendAsync(webobj);
	}
}