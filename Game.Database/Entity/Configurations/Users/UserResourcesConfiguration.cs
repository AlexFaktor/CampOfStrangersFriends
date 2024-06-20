﻿using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserResourcesConfiguration : IEntityTypeConfiguration<UserResources>
    {
        public void Configure(EntityTypeBuilder<UserResources> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.HasOne(x => x.User)
                   .WithOne(u => u.Resources)
                   .HasForeignKey<UserCamp>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.Property(x => x.RandomCoin)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Fackoins)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.SoulValue)
                .IsRequired();

            builder.Property(x => x.Energy)
                .IsRequired();
        }
    }
}