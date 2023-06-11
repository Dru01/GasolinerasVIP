namespace GasolinerasVIP.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UserInfo
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }
    }

    public partial class UserInfo
    {
        public static UserInfo FromJson(string json) => JsonConvert.DeserializeObject<UserInfo>(json, GasolinerasVIP.Models.Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this UserInfo self) => JsonConvert.SerializeObject(self, GasolinerasVIP.Models.Converter.Settings);
    }
}
