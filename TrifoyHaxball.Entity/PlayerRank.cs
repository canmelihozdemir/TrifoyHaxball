using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrifoyHaxball.Entity
{
    public class PlayerRank:BaseEntity
    {
        public string Name { get; set; }
        public int Point { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
