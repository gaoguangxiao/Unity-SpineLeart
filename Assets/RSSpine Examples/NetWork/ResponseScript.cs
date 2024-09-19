namespace QuickType
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Request
    /// </summary>
    public partial class RSResponse
    {
        [JsonProperty("code")]
        public long Code;

        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("actionList")]
        public ActionList[] ActionList { get; set; }

        [JsonProperty("expressionList")]
        public ExpressionList[] ExpressionList { get; set; }

        [JsonProperty("skinList")]
        public SkinList[] SkinList { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        //V2通过本地基本spine文件产生
        public string Atlas { get; set; }

        //JSON路径
        public string JSON { get; set; }

        //图片路径
        public string[] PNG { get; set; }

        //材质
        //public string Material { get; set; }

        //获取角色之后如果有部分皮肤
        //public bool isPartSKin { get; set; }
    }

    public partial class ActionList
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }
    }

    public partial class ExpressionList
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }
    }

    public partial class SkinList
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //拆分name,存储/前的皮肤名称
        [JsonProperty("allName")]
        public string AllName { get; set; }

        //拆分name,存储/后的皮肤名称
        [JsonProperty("subName")]
        public string SubName { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }
    }

    public partial class RSResponse
    {
        public static RSResponse FromJson(string json) => JsonConvert.DeserializeObject<RSResponse>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RSResponse self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
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
}