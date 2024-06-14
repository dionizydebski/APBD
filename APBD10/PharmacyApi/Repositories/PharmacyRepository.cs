using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyApi.Context;
using PharmacyApi.DTOs;
using PharmacyApi.Helpers;
using PharmacyApi.Models;

namespace PharmacyApi.Repositories;

public class PharmacyRepository : IPharmacyRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApbdContext _context;

    public PharmacyRepository(IConfiguration configuration, ApbdContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    
    public async Task<bool> DoesPatientExist(int idPatient)
    {
        return await _context.Patients.AnyAsync(x => x.IdPatient == idPatient);;
    }

    public async Task<Errors> AddPatient(PatientDTO patientDto)
    {
        var newPatient = new Patient
        {
            IdPatient = patientDto.IdPatient,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Birthdate = patientDto.BirthDate
        };

        await _context.Patients.AddAsync(newPatient);
        await _context.SaveChangesAsync();

        return Errors.Good;
    }

    public async Task<bool> DoesMedicamentExist(int idMedicament)
    {
        return await _context.Medicaments.AnyAsync(x => x.IdMedicament == idMedicament);
    }

    public async Task<int> AddPrescription(PrescriptionInDTO prescriptionInDto)
    {
        var newPrescription = new Prescription
        {
            Date = prescriptionInDto.Date,
            DueDate = prescriptionInDto.DueDate,
            IdPatient = prescriptionInDto.patient.IdPatient,
            IdDoctor = prescriptionInDto.IdDoctor
        };

        await _context.Prescriptions.AddAsync(newPrescription);
        await _context.SaveChangesAsync();

        return newPrescription.IdPrescription;
    }

    public async Task<Errors> AddPrescriptionMedicament(PrescriptionMedicamentDTO prescriptionMedicamentDto)
    {
        var newPrescriptionMedicament = new Prescription_Medicament
        {
            IdMedicament = prescriptionMedicamentDto.IdMedicament,
            IdPrescription = prescriptionMedicamentDto.IdPrescription,
            Dose = prescriptionMedicamentDto.Dose,
            Details = prescriptionMedicamentDto.Details
        };

        await _context.PrescriptionMedicaments.AddAsync(newPrescriptionMedicament);
        await _context.SaveChangesAsync();

        return Errors.Good;
    }

    public async Task<PatientDataDTO> GetPatientData(int IdPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicaments)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctors)
            .FirstOrDefaultAsync(p => p.IdPatient == IdPatient);

        PatientDataDTO patientDataDto = new PatientDataDTO
        (
            Patient: new PatientDTO(patient.IdPatient, patient.FirstName, patient.LastName, patient.Birthdate),
            Prescriptions: patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionOutDTO
                (
                    IdPrescription: p.IdPrescription,
                    Date: p.Date,
                    DueDate: p.DueDate,
                    Medicaments: p.PrescriptionMedicaments.Select(pm => new MedicamentDTO
                    (
                        IdMedicament: pm.Medicaments.IdMedicament,
                        Dose: pm.Dose,
                        Description: pm.Medicaments.Description 
                    )).ToList(),
                    Doctor: new DoctorDTO
                    (
                        p.Doctors.IdDoctor,
                        p.Doctors.FirstName
                    )
                )).ToList()
        );

        return patientDataDto;
    }

    public async Task<Errors> AddUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        
        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Errors.Good;
    }

    public async Task<TokenResponse> LoginUser(LoginRequest loginRequest)
    {
        AppUser user = await _context.Users.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();
    
        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
    
        if (passwordHashFromDb != curHashedPassword)
        {
            return null;
        }
    
    
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "dyzio"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();
        
        return new TokenResponse(
            accessToken: new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken: user.RefreshToken
            );
    }

    public async Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken)
    {
        AppUser user = await _context.Users.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "dyzio"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();
    
        return new TokenResponse(
            accessToken: new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken: user.RefreshToken
        );
    }
}