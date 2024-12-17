using System.Net;

namespace TechTestBackend.Domain.Exceptions;

public class InvalidIdException(string id) 
    : BaseException($"The id {id} is invalid", HttpStatusCode.BadRequest);