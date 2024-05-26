using tmp.ENUMs;
using TripApi.DTOs;

namespace TripApi.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<TripDTO>> GetTrips();
    Task<Errors> DeleteClient(int idClient);
    Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto);
}