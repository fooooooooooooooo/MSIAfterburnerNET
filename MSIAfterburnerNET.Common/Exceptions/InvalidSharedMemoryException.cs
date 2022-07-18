using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.Common.Exceptions;

[PublicAPI]
public class InvalidSharedMemoryException : Exception {
  private const string InvalidMessage = "Connected to invalid MSI Afterburner shared memory.";

  public InvalidSharedMemoryException() : base(InvalidMessage) { }

  public InvalidSharedMemoryException(Exception innerException) : base(InvalidMessage, innerException) { }

  public InvalidSharedMemoryException(string message) : base(message) { }

  public InvalidSharedMemoryException(string message, Exception innerException) : base(message, innerException) { }
}
