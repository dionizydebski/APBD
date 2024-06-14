using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PharmacyApi.DTOs;
using PharmacyApi.Helpers;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PharmacyController : ControllerBase
{
    private IPharmacyService _pharmacyService;
    
    public PharmacyController(IPharmacyService pharmacyService)
    {
        _pharmacyService = pharmacyService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionInDTO prescriptionInDto)
    {
        var result = await _pharmacyService.AddPrescription(prescriptionInDto);

        return result switch
        {
            Errors.NotFoundMecicament => NotFound("Nie znaleziono leku"),
            Errors.TooManyMedicaments => BadRequest("Za duzo leków"),
            Errors.WrongDate => BadRequest("Data musi byc starsza niz DueDate"),
            Errors.Good => Ok("Recepta zostala dodana"),
            _ => StatusCode(500, "Niezanany blad")
        };
    }
    
    [HttpGet("{idPatient:int}")]
    public async Task<IActionResult> GetPatientData(int idPatient)
    {
        // throw new Exception("Cos złego");
        return Ok(await _pharmacyService.GetPatientData(idPatient));
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var result = await _pharmacyService.RegisterUser(model);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _pharmacyService.LoginUser(loginRequest);

        if (result == null)
            return Unauthorized();

        return Ok(result);
    }
    
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var result = await _pharmacyService.RefreshUser(refreshToken);

        return Ok(result);
    }
}