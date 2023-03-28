using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Application.Common.Settings;

public static class JsonSerialization
{
	public static JsonSerializerSettings Settings => new() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, };
}