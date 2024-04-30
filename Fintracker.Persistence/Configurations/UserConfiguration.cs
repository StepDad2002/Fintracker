﻿using Fintracker.Application.BusinessRuleConstants;
using Fintracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintracker.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt).HasColumnType("date");
        builder.Property(x => x.ModifiedAt).HasColumnType("date");
        builder.Property(x => x.CreatedBy).HasMaxLength(50);
        builder.Property(x => x.ModifiedBy).HasMaxLength(50);

        builder.HasMany(x => x.OwnedBudgets)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(x => x.Categories)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.GlobalCurrency)
            .HasDefaultValue("USD");

        builder.OwnsOne(x => x.UserDetails, ba =>
        {
            ba.ToTable("UserDetails", ba =>
            {
                ba.HasCheckConstraint("CK_UserDetails_Birthday", "\"Birthday\" >= '1915-01-01'");
            });

            ba.Property(x => x.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Birthday");

            ba.Property(x => x.Avatar)
                .HasMaxLength(UserDetailsConstraints.MaxAvatarLength);
                
            ba.Property(x => x.Sex)
                .HasMaxLength(UserDetailsConstraints.MaxSexLength);

            ba.Property(x => x.Language)
                .HasConversion<string>();
        });
            
        
    }
}