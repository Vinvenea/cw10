using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }

        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }

        public CodeFirstContext()
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity => {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();

                var dicPatient = new List<Patient>();
                dicPatient.Add(new Models.Patient { IdPatient = 1, FirstName = "Wioletta", LastName = "Rozko", Birthdate =new DateTime(1987,5,2)});
                dicPatient.Add(new Models.Patient { IdPatient = 2, FirstName = "Tymon", LastName = "Wielak", Birthdate =new DateTime(1999, 8,5)});

                modelBuilder.Entity<Patient>().HasData(dicPatient);
            });

            modelBuilder.Entity<Doctor>(entity => {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                var dicDoc = new List<Doctor>();
                dicDoc.Add(new Models.Doctor { IdDoctor = 1, FirstName = "Urszula", LastName = "Wielka", Email = "blabla@lekarz.pl" });
                dicDoc.Add(new Models.Doctor { IdDoctor = 2, FirstName = "Kamil", LastName = "Sobor", Email = "blabla2@lekarz.pl" });
                modelBuilder.Entity<Doctor>().HasData(dicDoc);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_PK");
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
                entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions).HasForeignKey(d=>d.IdPatient).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Patient"); 
                entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions).HasForeignKey(d=>d.IdDoctor).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Doctor");
                var recepty = new List<Prescription>();
                recepty.Add(new Prescription { IdPrescription = 1, Date = DateTime.Now, DueDate = new DateTime(2020, 06,23), IdDoctor =1, IdPatient = 2});
                recepty.Add(new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = new DateTime(2020, 06,23), IdDoctor =2, IdPatient = 1});

                modelBuilder.Entity<Prescription>().HasData(recepty);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Type).IsRequired();

                var leki = new List<Medicament>();
                leki.Add(new Medicament { IdMedicament = 1, Name = "Nowy lek", Description = "Super dzialanie", Type = "Przeziebienie" });
                leki.Add(new Medicament { IdMedicament = 2, Name = "Tutu lek", Description = "Umiarkowane dzialanie", Type = "Przeziebienie" });
                modelBuilder.Entity<Medicament>().HasData(leki);
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                entity.HasAlternateKey(e => e.IdMedicament);
                entity.Property(d => d.Dose).IsRequired();
                entity.Property(d => d.Details).IsRequired();

               entity.HasOne(d => d.Medicament).WithMany(p => p.PrescriptionsMedicament).HasForeignKey(d => d.IdMedicament);
                entity.HasOne(d => d.Presc).WithMany(p => p.PrescriptionsMedicament).HasForeignKey(d => d.IdPrescription);

                

            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s18291;Integrated Security=True");
            }
        }
    }
}
