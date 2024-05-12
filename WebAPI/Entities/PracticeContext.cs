using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Entities;

public partial class PracticeContext : DbContext
{
    public PracticeContext()
    {
    }

    public PracticeContext(DbContextOptions<PracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCurrency> TbCurrencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCurrency>(entity =>
        {
            entity.HasKey(e => e.Currency).HasName("PK_Currency");

            entity.ToTable("TB_Currency");

            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
