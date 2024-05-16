using LAB06.Model;
using LAB06.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private IAnimalsService _animalsService;
    
    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }
    
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        var animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }
    
    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        var affectedCount = _animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPut("{id:int}/elo321/hujdpacyce")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var affectedCount = _animalsService.UpdateAnimal(animal);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var affectedCount = _animalsService.DeleteAnimal(id);
        return NoContent();
    }
}