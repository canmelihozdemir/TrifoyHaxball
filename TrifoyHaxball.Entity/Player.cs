using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrifoyHaxball.Entity
{
    public class Player:BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? PlayedGameCount { get; set; }
        public int? HighScore { get; set; }
        public int? Coin { get; set; }
        public PlayerRank? PlayerRank { get; set; }

    }
}
