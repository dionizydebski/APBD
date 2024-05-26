using tmp.ENUMs;
using TripApi.DTOs;

namespace TripApi.Services;

public interface ITripService
{
    Task<IEnumerable<TripDTO>> GetTrips();
    Task<Errors> DeleteClient(int idClient);
    Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto);
}