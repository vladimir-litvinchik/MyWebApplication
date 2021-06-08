using Newtonsoft.Json;
using System;
using System.Configuration;

namespace MyWebApplication.Viewer.Common.Config
{
    /// <summary>
    /// CommonConfiguration.
    /// </summary>
    public class CommonConfiguration : ConfigurationSection
    {
        [JsonProperty]
        public bool pageSelector { get; set; }

        [JsonProperty]
        public bool download { get; set; }

        [JsonProperty]
        public bool upload { get; set; }

        [JsonProperty] public bool print { get; set; } = true;

        [JsonProperty]
        public bool browse { get; set; }

        [JsonProperty]
        public bool rewrite { get; set; }

        [JsonProperty]
        public bool enableRightClick { get; set; }
    }
}