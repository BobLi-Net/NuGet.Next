﻿namespace NuGet.Next.Core.Exceptions;

public class ForbiddenException(string message) : Exception(message);