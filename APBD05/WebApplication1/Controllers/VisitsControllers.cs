using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/visits")]
[ApiController]
public class VisitsControllers : ControllerBase
{
    private static readonly List<Visit> _visits = new()
    {
    };

    [HttpGet("{id:int}")]
    public IActionResult GetVisits(int id)
    {
        List<Visit> result = new List<Visit>();
        
        foreach (var v in _visits)
        {
            if(v.IdAnimal == id)
                result.Add(v);
        }
        
        if (result.Count == 0)
        {
            return NotFound($"Visits was not found");
        }
        
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        _visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }
}