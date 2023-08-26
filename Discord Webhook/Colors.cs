namespace Discord.Webhook;

/// <summary>
///     List of pre-defined colors to use when building an embed
/// </summary>
public static class Colors
{
	/// <summary>
	///     Represents the RGB color '231, 76, 70' or HEX color '#e74c3c'
	/// </summary>
	public static readonly DColor Red = new(231, 76, 70); //https://www.spycolor.com/e74c3c#

	/// <summary>
	///     Represents the RGB color '153, 45, 34' or HEX color '#992d22'
	/// </summary>
	public static readonly DColor DarkRed = new(153, 45, 34); //https://www.spycolor.com/992d22#

	/// <summary>
	///     Represents the RGB color '155, 89, 182' or HEX color '#9b59b6'
	/// </summary>
	public static readonly DColor Purple = new(155, 89, 182); //https://www.spycolor.com/9b59b6#

	/// <summary>
	///     Represents the RGB color '46, 204, 113' or HEX color '#2ecc71'
	/// </summary>
	public static readonly DColor Green = new(46, 204, 113); //https://www.spycolor.com/2ecc71#

	/// <summary>
	///     Represents the RGB color '230, 126, 34' or HEX color '#e67e22'
	/// </summary>
	public static readonly DColor Orange = new(230, 126, 34); //https://www.spycolor.com/e67e22#

	/// <summary>
	///     Represents the RGB color '179, 177, 175' or HEX color '#b3b1af'
	/// </summary>
	public static readonly DColor Grey = new(179, 177, 175); //https://www.spycolor.com/b3b1af#

	/// <summary>
	///     Represents the RGB color '32, 102, 148' or HEX color '#206694'
	/// </summary>
	public static readonly DColor Blue = new(32, 102, 148); //https://www.spycolor.com/206694#

	/// <summary>
	///     Represents the RGB color '135, 206, 250' or HEX color '#87cefa'
	/// </summary>
	public static readonly DColor LightBlue = new(135, 206, 250); //https://www.spycolor.com/87cefa#

	/// <summary>
	///     Represents the RGB color '255, 255, 0' or HEX color '#ffff00'
	/// </summary>
	public static readonly DColor Yellow = new(255, 255, 0); //https://www.spycolor.com/ffff00#

	/// <summary>
	///     Represents the RGB color '64, 224, 208' or HEX color '#ee82ee'
	/// </summary>
	public static readonly DColor Violet = new(64, 224, 208); //https://www.spycolor.com/ee82ee#

	/// <summary>
	///     Represents the RGB color '160, 82, 45' or HEX color '#a0522d'
	/// </summary>
	public static readonly DColor Brown = new(160, 82, 45); //https://www.spycolor.com/a0522d#

	/// <summary>
	///     Represents the RGB color '255, 0, 255' or HEX color '#ff00ff'
	/// </summary>
	public static readonly DColor Magenta = new(255, 0, 255); //https://www.spycolor.com/ff00ff#

	/// <summary>
	///     Represents the RGB color '0, 0, 0' or HEX color '#ffffff'
	/// </summary>
	public static readonly DColor Black = new(0, 0, 0);

	/// <summary>
	///     Represents the RGB color '255, 255, 255' or HEX color '#000000'
	/// </summary>
	public static readonly DColor White = new(255, 255, 255);
}