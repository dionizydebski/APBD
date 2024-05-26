using tmp.ENUMs;
using TripApi.DTOs;
using TripApi.Repositories;

namespace TripApi.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    public async Task<IEnumerable<TripDTO>> GetTrips()
    {
        return await _tripRepository.GetTrips();
    }

    public async Task<Errors> DeleteClient(int idClient)
    {
        return await _tripRepository.DeleteClient(idClient);
    }

    public async Task<Errors> AssignClientToTrip(ClientInputDTO clientInputDto)
    {
        return await _tripRepository.AssignClientToTrip(clientInputDto);
    }
}