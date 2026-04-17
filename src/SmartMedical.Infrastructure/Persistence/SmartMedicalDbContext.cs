using Microsoft.EntityFrameworkCore;
using SmartMedical.Domain.Entities;

namespace SmartMedical.Infrastructure.Persistence;

public class SmartMedicalDbContext : DbContext
{
    public SmartMedicalDbContext(DbContextOptions<SmartMedicalDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalCondition> MedicalConditions { get; set; }
    public DbSet<PriorityScore> PriorityScores { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map Patient
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientID);
            entity.Property(e => e.FirstName).IsRequired().HasColumnType("VARCHAR(100)");
            entity.Property(e => e.LastName).IsRequired().HasColumnType("VARCHAR(100)");
            entity.Property(e => e.DateOfBirth).IsRequired().HasColumnType("DATE");
            entity.Property(e => e.ContactNum).HasColumnType("VARCHAR(20)");
        });

        // Map Appointment
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.DateTime).IsRequired().HasColumnType("DATETIME");
            entity.Property(e => e.Status).HasDefaultValue("Pending").HasMaxLength(20);
            entity.Property(e => e.UrgencyDescription).IsRequired().HasMaxLength(10);
            entity.Property(e => e.RequestedAt).HasDefaultValueSql("GETDATE()").HasColumnType("DATETIME");

            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(e => e.PatientID)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Map MedicalCondition
        modelBuilder.Entity<MedicalCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionID);
            entity.Property(e => e.ConditionName).IsRequired().HasColumnType("VARCHAR(255)");
            entity.Property(e => e.DiagnosisDate).HasColumnType("DATE");

            entity.HasOne(d => d.Patient)
                  .WithMany(p => p.MedicalConditions)
                  .HasForeignKey(d => d.PatientID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Map PriorityScore
        modelBuilder.Entity<PriorityScore>(entity =>
        {
            entity.HasKey(e => e.ScoreID);
            entity.Property(e => e.CalculationDate).HasDefaultValueSql("GETDATE()").HasColumnType("DATETIME");

            entity.HasOne(d => d.Appointment)
                  .WithOne(p => p.PriorityScore)
                  .HasForeignKey<PriorityScore>(d => d.AppointmentID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Map User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID);
            entity.Property(e => e.Username).IsRequired().HasColumnType("VARCHAR(100)");
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired().HasColumnType("VARCHAR(255)");
            entity.Property(e => e.Role).IsRequired().HasColumnType("VARCHAR(50)");
        });
    }
}
