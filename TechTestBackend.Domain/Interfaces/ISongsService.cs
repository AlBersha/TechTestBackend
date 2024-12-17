using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISongsService
{
    Task<List<SpotifySongModel>> GetTracksByNameAsync(string name);
    Task<List<SpotifySongModel>> GetAllLikedTracksAsync();
    Task AddTrackToLikedAsync(string id);
    Task RemoveTrackFromLikedAsync(string id);
}