﻿using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired()
        .HasColumnType("NVARCHAR")
        .IsRequired()
        .HasMaxLength(80);
        builder.Property(x => x.Type)
        .IsRequired()
        .HasColumnType("SMALLINT");
        builder.Property(x => x.Amount)
        .IsRequired()
        .HasColumnType("MONEY")
        .HasMaxLength(255);
        builder.Property(x => x.CreatedAt)
        .IsRequired();
        builder.Property(x => x.PaidOrReceivedAt)
        .IsRequired(false);
        builder.Property(x => x.UserId)
        .IsRequired()
        .HasColumnType("VARCHAR")
        .HasMaxLength(160);
    }
}
