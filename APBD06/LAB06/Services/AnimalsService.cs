using LAB06.Model;
using LAB06.Repositories;

namespace LAB06.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalsRepository.GetAnimals(orderBy);
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalsRepository.CreateAnimal(animal);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalsRepository.UpdateAnimal(animal);
    }

    public int DeleteAnimal(int idAnimal)
    {
        return _animalsRepository.DeleteAnimal(idAnimal);
    }
}