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
        public virtual DbSet<Dislikes> Dislikes { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<Proposals> Proposals { get; set; }
        public virtual DbSet<StageReceivers> StageReceivers { get; set; }
        public virtual DbSet<Stages> Stages { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserTypes> UserTypes { get; set; }
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

            modelBuilder.Entity<Dislikes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dislikes_Proposals");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dislikes_Users");
            });

            modelBuilder.Entity<Likes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Likes_Proposals");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Likes_Users");
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

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RejectReason).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CurrentStage)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.CurrentStageId)
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

                entity.HasOne(d => d.Vacation)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.VacationId)
                    .HasConstraintName("FK_Proposals_Vacations");
            });

            modelBuilder.Entity<StageReceivers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.StageReceivers)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StageReceivers_Stages");
            });

            modelBuilder.Entity<Stages>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<Stages>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stages_Stages");

                entity.HasOne(d => d.NextStage)
                    .WithMany(p => p.InverseNextStage)
                    .HasForeignKey(d => d.NextStageId)
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

            modelBuilder.Entity<UserTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTypes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTypes_Users");
            });

            modelBuilder.Entity<Vacations>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(255);

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
