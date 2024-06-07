using System.ComponentModel.DataAnnotations;

namespace PharmacyApi.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}