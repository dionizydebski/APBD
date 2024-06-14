using PharmacyApi.DTOs;
using PharmacyApi.Models;

namespace PharmacyApi.Services;

public interface IPharmacyService
{
    Task<Errors> AddPrescription(PrescriptionInDTO prescriptionInDto);
    Task<PatientDataDTO> GetPatientData(int IdPatient);
    
    Task<Errors> RegisterUser(RegisterRequest model); 
    Task<TokenResponse> LoginUser(LoginRequest loginRequest); 
    Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken); 
}