using PharmacyApi.DTOs;

namespace PharmacyApi.Services;

public interface IPharmacyService
{
    Task<Errors> AddPrescription(PrescriptionInDTO prescriptionInDto);
    Task<PatientDataDTO> GetPatientData(int IdPatient);
}