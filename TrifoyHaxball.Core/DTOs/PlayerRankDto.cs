using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Core.DTOs
{
    public class PlayerRankDto:BaseDto
    {
        public string Name { get; set; }
        public int Point { get; set; }
    }
}
