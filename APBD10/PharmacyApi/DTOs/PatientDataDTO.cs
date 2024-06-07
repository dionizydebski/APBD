namespace PharmacyApi.DTOs;

public record PatientDataDTO(PatientDTO Patient, List<PrescriptionOutDTO> Prescriptions);