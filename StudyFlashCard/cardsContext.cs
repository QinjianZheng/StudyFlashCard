using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudyFlashCard
{
    public partial class cardsContext : DbContext
    {
        public cardsContext()
        {
        }

        public cardsContext(DbContextOptions<cardsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=../cards.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .HasColumnName("id");

                entity.Property(e => e.Back)
                    .HasColumnType("text")
                    .HasColumnName("back");

                entity.Property(e => e.Front)
                    .HasColumnType("text")
                    .HasColumnName("front");

                entity.Property(e => e.Known)
                    .HasColumnType("boolean")
                    .HasColumnName("known")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint")
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
