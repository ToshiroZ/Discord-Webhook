using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Discord.Webhook;

/// <summary>
///     The main JSON serializer for the library
/// </summary>
/// <typeparam name="TType">The type to be serializing the responses to</typeparam>
internal static class JsonSerializer<TType> where TType : class
{
	/// <summary>
	///     Serializes an object to JSON
	/// </summary>
	public static string Serialize(TType instance)
	{
		var serializer = new DataContractJsonSerializer(typeof(TType));
		using var stream = new MemoryStream();

		serializer.WriteObject(stream, instance);
		return Encoding.Default.GetString(stream.ToArray());
	}

	/// <summary>
	///     Deserializes an object from JSON
	/// </summary>
	public static TType? Deserialize(string json)
	{
		using var stream = new MemoryStream(Encoding.Default.GetBytes(json));

		var serializer = new DataContractJsonSerializer(typeof(TType));
		return serializer.ReadObject(stream) as TType;
	}
}