using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrifoyHaxball.Core.DTOs
{
    public class PlayerWithRankDto:PlayerDto
    {
        public PlayerRankDto PlayerRank { get; set; }
    }
}
