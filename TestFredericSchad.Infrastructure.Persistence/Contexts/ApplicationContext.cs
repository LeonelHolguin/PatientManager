using Microsoft.EntityFrameworkCore;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Domain.Common;
using PatientManager.Core.Domain.Entities;
using System.Reflection;
using System.Reflection.Emit;

namespace PatientManager.Infrastructure.Persistence.Contexts;

public partial class ApplicationContext : DbContext
{
    private readonly ICurrentUserApp _currentUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, ICurrentUserApp currentUser) : base(options) 
    { 
        _currentUser = currentUser;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>()) 
        { 
            switch(entry.State) 
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.CreatedBy = _currentUser.GetCurrentUserName();
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    entry.Entity.LastModifiedBy = _currentUser.GetCurrentUserName();
                    break;
            
            }
        
        }

        return base.SaveChangesAsync(cancellationToken);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Medic> Medics { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
    public DbSet<LaboratoryTestResult> LaboratoryTestResults { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Flient API
        #region tables
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Medic>().ToTable("Medics");
        modelBuilder.Entity<Patient>().ToTable("Patients");
        modelBuilder.Entity<LaboratoryTest>().ToTable("LaboratoryTests");
        modelBuilder.Entity<LaboratoryTestResult>().ToTable("LaboratoryTestResults");
        modelBuilder.Entity<Appointment>().ToTable("Appointments");
        #endregion

        #region "primary keys"
        modelBuilder.Entity<User>().HasKey(user => user.Id); //lambda
        modelBuilder.Entity<Medic>().HasKey(medic => medic.Id);
        modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
        modelBuilder.Entity<LaboratoryTest>().HasKey(laboratoryTest => laboratoryTest.Id);
        modelBuilder.Entity<LaboratoryTestResult>().HasKey(laboratoryTestResult => laboratoryTestResult.Id);
        modelBuilder.Entity<Appointment>().HasKey(appointment => appointment.Id);
        #endregion

        #region relationships
        modelBuilder.Entity<Patient>()
            .HasMany<LaboratoryTestResult>(patient => patient.LaboratoryTestResults)
            .WithOne(laboratoryTestResult => laboratoryTestResult.Patient)
            .HasForeignKey(laboratoryTestResult => laboratoryTestResult.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Patient>()
            .HasMany<Appointment>(patient => patient.Appointments)
            .WithOne(appointment => appointment.Patient)
            .HasForeignKey(appointment => appointment.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Medic>()
            .HasMany<Appointment>(medic => medic.Appointments)
            .WithOne(appointment => appointment.Medic)
            .HasForeignKey(appointment => appointment.MedicId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LaboratoryTest>()
            .HasMany<LaboratoryTestResult>(laboratoryTest => laboratoryTest.LaboratoryTestResults)
            .WithOne(laboratoryTestResult => laboratoryTestResult.LaboratoryTest)
            .HasForeignKey(laboratoryTestResult => laboratoryTestResult.LaboratoryTestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Appointment>()
            .HasMany<LaboratoryTestResult>(appointment => appointment.LaboratoryTestResults)
            .WithOne(laboratoryTestResult => laboratoryTestResult.Appointment)
            .HasForeignKey(laboratoryTestResult => laboratoryTestResult.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region "Property configurations"

        #region User
        modelBuilder.Entity<User>().Property(p => p.Name)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<User>().Property(p => p.LastName)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<User>().Property(p => p.Email)
            .IsRequired().HasMaxLength(100);

        modelBuilder.Entity<User>().Property(p => p.UserName)
    .IsRequired().HasMaxLength(25);

        modelBuilder.Entity<User>().Property(p => p.Password)
            .IsRequired();

        modelBuilder.Entity<User>().Property(p => p.Role)
            .IsRequired().HasMaxLength(20);

        modelBuilder.Entity<User>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<User>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<User>().Property(p => p.LastModified);

        modelBuilder.Entity<User>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion

        #region Patient
        modelBuilder.Entity<Patient>().Property(p => p.Name)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<Patient>().Property(p => p.LastName)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<Patient>().Property(p => p.Phone)
            .IsRequired().HasMaxLength(15);

        modelBuilder.Entity<Patient>().Property(p => p.Address)
            .IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Patient>().Property(p => p.IdentityCard)
            .IsRequired().HasMaxLength(13);

        modelBuilder.Entity<Patient>().Property(p => p.BirthDate)
            .IsRequired();

        modelBuilder.Entity<Patient>().Property(p => p.IsSmoker)
            .IsRequired();

        modelBuilder.Entity<Patient>().Property(p => p.HasAllergy)
            .IsRequired();

        modelBuilder.Entity<Patient>().Property(p => p.Photo)
            .IsRequired();

        modelBuilder.Entity<Patient>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<Patient>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<Patient>().Property(p => p.LastModified);

        modelBuilder.Entity<Patient>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion

        #region Medic
        modelBuilder.Entity<Medic>().Property(p => p.Name)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<Medic>().Property(p => p.LastName)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<Medic>().Property(p => p.Phone)
            .IsRequired().HasMaxLength(15);

        modelBuilder.Entity<Medic>().Property(p => p.Email)
            .IsRequired().HasMaxLength(50);

        modelBuilder.Entity<Medic>().Property(p => p.IdentityCard)
            .IsRequired().HasMaxLength(13);

        modelBuilder.Entity<Medic>().Property(p => p.Photo)
            .IsRequired();

        modelBuilder.Entity<Medic>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<Medic>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<Medic>().Property(p => p.LastModified);

        modelBuilder.Entity<Medic>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion

        #region LaboratoryTest
        modelBuilder.Entity<LaboratoryTest>().Property(p => p.Name)
            .IsRequired().HasMaxLength(50);

        modelBuilder.Entity<LaboratoryTest>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<LaboratoryTest>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<LaboratoryTest>().Property(p => p.LastModified);

        modelBuilder.Entity<LaboratoryTest>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion

        #region LaboratoryTestResult
        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.Description);

        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.State)
            .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.LastModified);

        modelBuilder.Entity<LaboratoryTestResult>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion

        #region Appointment
        modelBuilder.Entity<Appointment>().Property(p => p.AppointmentDate)
            .IsRequired();

        modelBuilder.Entity<Appointment>().Property(p => p.AppointmentTime)
            .IsRequired();

        modelBuilder.Entity<Appointment>().Property(p => p.Reason)
            .IsRequired();

        modelBuilder.Entity<Appointment>().Property(p => p.State)
    .IsRequired().HasMaxLength(30);

        modelBuilder.Entity<Appointment>().Property(p => p.Created)
            .IsRequired();

        modelBuilder.Entity<Appointment>().Property(p => p.CreatedBy)
            .IsRequired().HasMaxLength(60);

        modelBuilder.Entity<Appointment>().Property(p => p.LastModified);

        modelBuilder.Entity<Appointment>().Property(p => p.LastModifiedBy)
            .HasMaxLength(60);
        #endregion


        #endregion
    }
}

