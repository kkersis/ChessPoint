using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LichessPlayer
    {
        [JsonProperty("user")]
        public LichessUser User { get; set; }
        public int Rating { get; set; }
        public int? AiLevel { get; set; }
    }

    public class LichessUser
    {
        [JsonProperty("id")]
        public string Username { get; set; }
    }
}
