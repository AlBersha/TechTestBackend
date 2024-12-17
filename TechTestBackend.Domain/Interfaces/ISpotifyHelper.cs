using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain.Interfaces;

public interface ISpotifyHelper
{
    Task<List<SpotifySongModel>> GetTracksByNameAsync(string name);
    Task<SpotifySongModel> GetTrackById(string id);
}