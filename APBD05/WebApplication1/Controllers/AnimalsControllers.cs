using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsControllers : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { IdAnimal = 1, Name = "Aro", Category = "Pies", Weight = 15.2, FurColor = "Czarny" },
        new Animal { IdAnimal = 2, Name = "Ida", Category = "Kot", Weight = 6.7, FurColor = "Biało-szary" },
        new Animal { IdAnimal = 3, Name = "Zara", Category = "Małpa", Weight = 10.3, FurColor = "Brązowo-czarny" }
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(ani => ani.IdAnimal == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
        
        return Ok(animal);
    }
    
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult EditAnimal(int id, Animal animal)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.IdAnimal == id);

        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToDelete= _animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animalToDelete == null)
        {
            return NoContent();
        }

        _animals.Remove(animalToDelete);
        return NoContent();
    }
}