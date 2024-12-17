using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISongsService
{
    Task<List<SpotifySongModel>> GetTracksByNameAsync(string name);
    Task AddTrackToLickedAsync(string id);
    Task RemoveTrackFromLickedAsync(string id);
    Task<List<SpotifySongModel>> GetAllLikedTracksAsync();
}