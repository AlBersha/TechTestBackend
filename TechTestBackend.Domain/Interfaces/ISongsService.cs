using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISongsService
{
    Task<List<SpotifySongModel>> GetTracksByNameAsync(string name);
    Task AddTrackToLikedAsync(string id);
    Task RemoveTrackFromLikedAsync(string id);
    Task<List<SpotifySongModel>> GetAllLikedTracksAsync();
}