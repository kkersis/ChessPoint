using Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ChesscomGame
    {
        public string Url { get; set; }
        public string Pgn { get; set; }
        public OpeningInfo Opening { get; set; }
        public ServerType Server { get; } = ServerType.CHESSCOM;
        [JsonProperty("end_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime EndTime { get; set; }
        [JsonProperty("white")]
        public ChessComPlayer WhitePlayer { get; set; }
        [JsonProperty("black")]
        public ChessComPlayer BlackPlayer { get; set; }
        public EndingType Ending { get; set; }
        public ColorType MyColor { get; set; }
        [JsonProperty("time_class")]
        public string TimeClass { get; set; }
    }
}
