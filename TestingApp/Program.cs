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
					"https://discord.com/api/webhooks/1144978232840573100/9p1EXYwMek0c3y9dUfPr4PGK3_B9dzASIsuSoufj0_i3o5FXTrpOvY11DL2grvMi2T9C");
		});

		webobj.AddEmbed(new EmbedBuilder()
			.WithTitle("Discord-Webhook lib second message")
			.WithDescription("Testing discord Webhook")
			.WithColor(Colors.Orange)
		);

		await new Webhook(url, "Webhook library test", "https://www.thegatewaypundit.com/wp-content/uploads/trump-mugshot-1.jpg", "test-thread").SendAsync(webobj);
	}
}