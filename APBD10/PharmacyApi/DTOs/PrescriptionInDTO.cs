namespace PharmacyApi.DTOs;

public record PrescriptionInDTO(PatientDTO patient, List<MedicamentDTO> medicaments,int IdDoctor , DateTime Date, DateTime DueDate);