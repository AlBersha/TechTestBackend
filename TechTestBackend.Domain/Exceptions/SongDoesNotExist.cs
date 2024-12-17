using System.Net;

namespace TechTestBackend.Domain.Exceptions;

public class SongDoesNotExist(string songId)
    : BaseException($"Song with id {songId} does not exist", HttpStatusCode.NotFound);