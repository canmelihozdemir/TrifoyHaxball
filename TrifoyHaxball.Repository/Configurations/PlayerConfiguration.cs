using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Repository.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Role).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.PlayedGameCount).IsRequired(false);
            builder.Property(x => x.HighScore).IsRequired(false);
            builder.Property(x => x.Coin).IsRequired(false);
            builder.ToTable("Players");

        }
    }
}
