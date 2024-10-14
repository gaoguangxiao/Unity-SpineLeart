using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// Data user T
/// </summary>
/// <typeparam name="T"></typeparam>
public class RSResponseV2<T>
{
    [JsonProperty("code")]
    public long Code;

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("msg")]
    public string Msg { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }
}

/// <summary>
/// Data user T
/// </summary>
/// <typeparam name="T"></typeparam>
public class RSResponseV3<T>
{
    [JsonProperty("code")]
    public long Code;

    [JsonProperty("data")]
    public T[] Data { get; set; }

    [JsonProperty("msg")]
    public string Msg { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }
}

public class RSResponseV2
{
    public static RSResponseV2 FromJson(string json) => JsonConvert.DeserializeObject<RSResponseV2>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this RSResponseV2 self) => JsonConvert.SerializeObject(self);
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}


