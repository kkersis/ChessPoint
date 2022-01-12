using Core.Entities;
using Core.Enums;
using System;

namespace Domain.Models
{
    public class Game
    {
        public string Url { get; set; }
        public OpeningInfo Opening { get; set; }
        public ServerType Server { get; set; }
        public DateTime Date { get; set; }
        public EndingType Ending { get; set; }
        public GamePlayer Me { get; set; }
        public GamePlayer Opponent { get; set; }
        public ColorType MyColor { get; set; }
        public string TimeClass { get; set; }
    }
}
