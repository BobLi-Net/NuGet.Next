namespace NuGet.Next.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);