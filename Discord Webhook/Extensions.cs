using System.Drawing;

namespace Discord.Webhook
{
	public static class Extensions
	{
        /// <summary>
        ///     Converts a <see cref="System.Drawing.Color" /> to a <see cref="DColor" /> to be used in an embed
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <returns>The converted color</returns>
        public static DColor ToDColor(this Color color)
		{
			return new DColor(color.R, color.G, color.B);
		}
	}
}