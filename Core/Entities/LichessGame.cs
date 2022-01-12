using Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LichessGame
    {
        public string Id { get; set; }
        public string Variant { get; set; }
        public OpeningInfo Opening { get; set; }
        public ServerType Server { get; } = ServerType.LICHESSORG;
        [JsonProperty("createdAt")]
        public long TimeInMiliSec { get; set; }
        public string Winner { get; set; }
        public LichessPlayers Players { get; set; }
        [JsonProperty("speed")]
        public string TimeClass { get; set; }
    }

    public class LichessPlayers
    {
        public LichessPlayer White { get; set; }
        public LichessPlayer Black { get; set; }
    }

}
