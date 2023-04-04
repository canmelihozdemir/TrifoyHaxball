using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Repository.Configurations
{
    public class PlayerRankConfiguration : IEntityTypeConfiguration<PlayerRank>
    {
        public void Configure(EntityTypeBuilder<PlayerRank> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Player).WithOne(x => x.PlayerRank).HasForeignKey<PlayerRank>(x => x.PlayerId);
        }
    }
}
