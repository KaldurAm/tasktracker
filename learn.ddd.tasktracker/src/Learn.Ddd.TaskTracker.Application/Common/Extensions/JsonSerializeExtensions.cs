using Learn.Ddd.TaskTracker.Application.Common.Settings;
using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Application.Common.Extensions;

public static class JsonSerializeExtensions
{
	public static string ToJson<T>(this T source)
		where T : class
	{
		return JsonConvert.SerializeObject(source, JsonSerialization.Settings);
	}

	public static T? ConvertTo<T>(this string? json)
		where T : class
	{
		return json is null ?
			JsonConvert.DeserializeObject<T>(json!, JsonSerialization.Settings) :
			throw new ArgumentNullException(json);
	}
}