using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Webhook
{
    /// <summary>
    ///     The class that handles sending requests to the discord webhook api
    /// </summary>
    public class Webhook
	{
		private static readonly HttpClient _webhookClient = new HttpClient();
		private readonly string _url;
		private readonly string _username;
		private readonly string _avatarUrl;

        /// <summary>
        ///     The constructor for the webhook
        /// </summary>
        /// <param name="webhookUrl">The url for the webhook</param>
        /// <param name="username">
        ///     The username the webhook will send with if its null defaults to what was assigned in the webhook
        ///     menu on discord
        /// </param>
        /// <param name="avatarUrl">
        ///     The url to the picture the webhook will use if its null defaults to what was assigned in the
        ///     webhook menu on discord
        /// </param>
        public Webhook(string webhookUrl, string username = null, string avatarUrl = null)
		{
			_url = webhookUrl;
			_username = username;
			_avatarUrl = avatarUrl;
		}

        /// <summary>
        ///     Sends a message to the discord webhook
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
	        var contents = new Dictionary<string, string>
	        {
		        { "content", message },
		        { "username", _username },
		        { "avatar_url", _avatarUrl }
	        };

	        _webhookClient.PostAsync(_url, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
        }

        /// <summary>
        ///     sends a webhookobject to the discord webhook used mostly for embeds
        /// </summary>
        /// <param name="obj">the webhook object</param>
        public void Send(WebhookObject obj)
        {
	        obj.username = _username ?? obj.username;
	        obj.avatar_url = _username == null ? obj.avatar_url : _avatarUrl;
	        var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

	        _webhookClient.PostAsync(_url, json).GetAwaiter().GetResult();
        }

        /// <summary>
        ///     deletes the webhook
        /// </summary>
        public void Delete()
		{
			try
			{
				_webhookClient.DeleteAsync(_url).GetAwaiter().GetResult();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

        /// <summary>
        ///     send a message to the discord webhook asynchronously
        /// </summary>
        /// <param name="message"></param>
        /// <returns>the message to send</returns>
        public async Task SendAsync(string message)
		{
			try
			{
				var contents = new Dictionary<string, string>
				{
					{ "content", message },
					{ "username", _username },
					{ "avatar_url", _avatarUrl }
				};

				await _webhookClient.PostAsync(_url, new FormUrlEncodedContent(contents));
			}
			catch (Exception e)
			{
				throw e;
			}
		}

        /// <summary>
        ///     Send a webhook object to the discord api asynchronously
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task SendAsync(WebhookObject obj)
		{
			try
			{
				var json = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");

				await _webhookClient.PostAsync(_url, json);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

        /// <summary>
        ///     Deletes the webhook asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAsync()
        {
	        await _webhookClient.DeleteAsync(_url);
        }
	}
}