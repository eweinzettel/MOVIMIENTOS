using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovimeintoWebAPI.Models
{
    public partial class MOVIMIENTOSContext : DbContext
    {
        public MOVIMIENTOSContext()
        {
        }

        public MOVIMIENTOSContext(DbContextOptions<MOVIMIENTOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Central> Centrals { get; set; } = null!;
        public virtual DbSet<Oficina> Oficinas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Central>(entity =>
            {
                entity.HasKey(e => e.IdCentral);

                entity.ToTable("Central");

                entity.Property(e => e.CentralIndicativo)
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.CentralNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Oficina>(entity =>
            {
                entity.HasKey(e => e.IdOficina);

                entity.ToTable("Oficina");

                entity.Property(e => e.IdOficina).ValueGeneratedNever();

                entity.Property(e => e.OficinaIndicativo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.OficinaNombre)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.OficinaTipo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Central)
                    .WithMany(p => p.Oficinas)
                    .HasForeignKey(d => d.CentralId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oficina_Central");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
