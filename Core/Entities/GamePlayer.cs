using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GamePlayer
    {
        public string Username { get; set; }
        public ColorType Color { get; set; }
        public int Rating { get; set; }
    }
}
