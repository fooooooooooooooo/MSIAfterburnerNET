using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.Common.Exceptions;

[PublicAPI]
public class SharedMemoryNotFoundException : Exception {
  private const string ConnectionFailedMessage = "Could not connect to MSI Afterburner shared memory.";

  public SharedMemoryNotFoundException() : base(ConnectionFailedMessage) { }

  public SharedMemoryNotFoundException(Exception innerException) : base(ConnectionFailedMessage, innerException) { }

  public SharedMemoryNotFoundException(string message) : base(message) { }

  public SharedMemoryNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
