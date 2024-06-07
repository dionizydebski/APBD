using Microsoft.EntityFrameworkCore;
using PharmacyApi.Models;

namespace PharmacyApi.Context;

public partial class ApbdContext : DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options) : base(options)
    {
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    
    public DbSet<Patient> Patients { get; set; }
    
    public DbSet<Prescription> Prescriptions { get; set; }
    
    public DbSet<Medicament> Medicaments { get; set; }
    
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=APBD;Integrated Security=True;Encrypt=False");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });
        
        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Prescriptions)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);
        
        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Medicaments)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
    }
}