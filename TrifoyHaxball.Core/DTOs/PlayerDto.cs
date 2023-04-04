using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Core.DTOs
{
    public class PlayerDto:BaseDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int PlayedGameCount { get; set; }
        public int HighScore { get; set; }
        public int Coin { get; set; }
    }
}
