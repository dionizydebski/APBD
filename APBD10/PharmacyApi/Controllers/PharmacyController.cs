using Microsoft.AspNetCore.Mvc;
using PharmacyApi.DTOs;
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
        return Ok(await _pharmacyService.GetPatientData(idPatient));
    }
}