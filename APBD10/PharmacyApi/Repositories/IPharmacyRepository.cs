using PharmacyApi.DTOs;
using PharmacyApi.Models;

namespace PharmacyApi.Repositories;

public interface IPharmacyRepository
{
    Task<bool> DoesPatientExist(int idPatient);
    Task<Errors> AddPatient(PatientDTO patientDto);
    
    Task<bool> DoesMedicamentExist(int idMedicament);
    
    Task<int> AddPrescription(PrescriptionInDTO prescriptionInDto);
    
    Task<Errors> AddPrescriptionMedicament(PrescriptionMedicamentDTO prescriptionMedicamentDto);
    
    Task<PatientDataDTO> GetPatientData(int IdPatient);
    
    Task<Errors> AddUser(RegisterRequest model);
    Task<TokenResponse> LoginUser(LoginRequest loginRequest); 
    Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken); 
}