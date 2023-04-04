using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Repository.Seeds
{
    internal class PlayerRankSeed : IEntityTypeConfiguration<PlayerRank>
    {
        public void Configure(EntityTypeBuilder<PlayerRank> builder)
        {
            
        }
    }
}
