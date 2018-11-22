using System;
using GlassProposalsApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GlassProposalsApp.Data
{
    public partial class GlassProposalContext : DbContext
    {
        public GlassProposalContext()
        {
        }

        public GlassProposalContext(DbContextOptions<GlassProposalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bonuses> Bonuses { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<Proposals> Proposals { get; set; }
        public virtual DbSet<Stages> Stages { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vacations> Vacations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bonuses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bonuses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bonuses_Users");
            });

            modelBuilder.Entity<Processes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Proposals>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CurrentStage)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.CurrentStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposals_Stages");

                entity.HasOne(d => d.Initiator)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.InitiatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposals_Initiators");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposals_Processes");
            });

            modelBuilder.Entity<Stages>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.NextStage)
                    .WithMany(p => p.InverseNextStage)
                    .HasForeignKey(d => d.NextStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stages_NextStages");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.Stages)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stages_Processes");
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.DecisionMaker)
                    .WithMany(p => p.Statuses)
                    .HasForeignKey(d => d.DecisionMakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Statuses_DecisionMakers");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Statuses)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Statuses_Proposals");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.InverseMentor)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("FK_Users_Mentors");
            });

            modelBuilder.Entity<Vacations>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vacations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vacations_Users");
            });
        }
    }
}
