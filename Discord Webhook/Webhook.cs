using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Webhook
{
	/// <summary>
	///     The class that handles sending requests to the Discord webhook
	/// </summary>
	public class Webhook
	{
		private static readonly HttpClient WebhookClient = new HttpClient();
		private readonly string _url;
		private readonly string _username;
		private readonly string _avatarUrl;

		/// <summary>
		///     Constructs a new Discord webhook object
		/// </summary>
		/// <param name="webhookUrl">The Discord webhook URL</param>
		/// <param name="username">
		///     The username the webhook will send with, if its null defaults to what was assigned in the webhook
		///     menu on Discord
		/// </param>
		/// <param name="avatarUrl">
		///     The URL that points to the avatar image the webhook will use, if its null defaults to what was assigned in the
		///     webhook menu on Discord
		/// </param>
		public Webhook(string webhookUrl, string username = null, string avatarUrl = null)
		{
			_url = webhookUrl;
			_username = username;
			_avatarUrl = avatarUrl;
		}

		/// <summary>
		///     Sends a message to the Discord webhook synchronously
		/// </summary>
		/// <param name="message">The string message to send to the webhook</param>
		public void Send(string message)
		{
			var contents = new Dictionary<string, string>
			{
				{ "content", message },
				{ "username", _username },
				{ "avatar_url", _avatarUrl }
			};

			WebhookClient.PostAsync(_url, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
		}

		/// <summary>
		///     Sends a webhook object to the Discord webhook synchronously
		/// </summary>
		/// <param name="obj">The object to send to the webhook</param>
		public void Send(WebhookObject obj)
		{
			obj.username = _username ?? obj.username;
			obj.avatar_url = _username == null ? obj.avatar_url : _avatarUrl;
			var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

			WebhookClient.PostAsync(_url, json).GetAwaiter().GetResult();
		}

		/// <summary>
		///     Deletes the webhook
		/// </summary>
		public void Delete()
		{
			WebhookClient.DeleteAsync(_url).GetAwaiter().GetResult();
		}

		/// <summary>
		///     Sends a message to the Discord webhook asynchronously
		/// </summary>
		/// <param name="message">The string message to send to the webhook</param>
		/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
		public async Task SendAsync(string message)
		{
			var contents = new Dictionary<string, string>
			{
				{ "content", message },
				{ "username", _username },
				{ "avatar_url", _avatarUrl }
			};

			await WebhookClient.PostAsync(_url, new FormUrlEncodedContent(contents));
		}

		/// <summary>
		///     Sends a webhook object to the Discord webhook asynchronously
		/// </summary>
		/// <param name="obj">The object to send to the webhook</param>
		/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
		public async Task SendAsync(WebhookObject obj)
		{
			var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

			await WebhookClient.PostAsync(_url, json);
		}

		/// <summary>
		///     Deletes the webhook asynchronously
		/// </summary>
		/// <returns>A <see cref="Task" /> representing the status of the operation</returns>
		public async Task DeleteAsync()
		{
			await WebhookClient.DeleteAsync(_url);
		}
	}
}