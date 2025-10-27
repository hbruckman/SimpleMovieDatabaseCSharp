namespace Smdb.Core.Shared;

using System.Text.Json;

public static class JsonUtils
{
	public static JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
	{
		PropertyNameCaseInsensitive = true,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};
}
