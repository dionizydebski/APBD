using LAB06.Model;

namespace LAB06.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int idAnimal);
}