using Microsoft.Extensions.Logging;
using TechTestBackend.Domain.Exceptions;
using TechTestBackend.Domain.Interfaces;
using TechTestBackend.Domain.Models;

namespace TechTestBackend.Domain;

public class SongsService(ISongsRepository songsRepository, ISpotifyHelper spotifyHelper, ILogger<SongsService> logger) : ISongsService
{
    public async Task<List<SpotifySongModel>> GetTracksByNameAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return new List<SpotifySongModel>();
        }

        var tracks = await spotifyHelper.GetTracksByNameAsync(name);
        if (tracks.Any() == false)
        {
            throw new Exception("No tracks have been received from Spotify API.");
        }

        return tracks;
    }

    public async Task AddTrackToLikedAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            logger.LogError("Fail to add track to likes - id is null or empty.");
            throw new InvalidIdException(id);
        }

        var song = await spotifyHelper.GetTrackByIdAsync(id);
        if (song == null || song.Id == null)
        {
            logger.LogError("Fail to find the track.");
            throw new SongDoesNotExist(id);
        }

        await songsRepository.AddTrackToLikedAsync(song);
    }

    public async Task RemoveTrackFromLikedAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            logger.LogError("Fail to remove track from likes - id is null or empty.");
            throw new InvalidIdException(id);
        }

        var song = await songsRepository.GetTrackById(id);
        if (song == null)
        {
            logger.LogError("Fail to remove track from likes - the song does not exist in the database.");
            throw new SongDoesNotExist(id);
        }
        await songsRepository.RemoveTrackFromLikedAsync(song);
    }

    public async Task<List<SpotifySongModel>> GetAllLikedTracksAsync()
    {
        return await songsRepository.GetAllLikedTracksAsync();
    }
}