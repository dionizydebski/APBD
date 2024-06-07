using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApi.Models;

public class Prescription_Medicament
{
    [Key, Column(Order = 0)]
    public int IdMedicament { get; set; }
    
    [Key, Column(Order = 1)]
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Details { get; set; }
    
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicaments { get; set; }
    
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescriptions { get; set; }
}