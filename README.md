# Discord.Webhook

Used for sending webhooks to  discord using the discord webhook url. It is also dependency free.

[![NuGet version](https://badge.fury.io/nu/Discord.Webhook.svg)](https://badge.fury.io/nu/Discord.Webhook)

### Sample

#### Creating  WebhookObject

```c#
var webobj = new WebhookObject();
```

#### Building an embed object

```c#
webobj.AddEmbed(builder =>
            {
                builder.WithTitle("Discord-Webhook lib")
                    .WithDescription("Building embed with 'AddEmbed(Action<EmbedBuilder> embedBuilderFunction)'")
                    .WithUrl("https://github.com/ToshiroZ/Discord-Webhook")                    	 .WithThumbnail("https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png")
                    .WithColor(Colors.Magenta)
                    .WithImage("https://i.imgur.com/ZGPxFN2.jpg")
                    .AddField("New Field", "This is a new field")
                    .WithFooter("DiscordUser", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Font_Awesome_5_brands_discord_color.svg/800px-Font_Awesome_5_brands_discord_color.svg.png");
            });
```

#### Sending Message to the webhook

```c#
await new Webhook(url).SendAsync(webobj);
```

#### Result

![Screenshot](/Resources/sample1.png?raw=true)



