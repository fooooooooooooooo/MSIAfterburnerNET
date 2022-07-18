using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.Common.Exceptions;

[PublicAPI]
public class DeadSharedMemoryException : Exception {
  private const string DeadMessage = "Connected to MSI Afterburner shared memory that is flagged as dead.";

  public DeadSharedMemoryException() : base(DeadMessage) { }

  public DeadSharedMemoryException(Exception innerException) : base(DeadMessage, innerException) { }

  public DeadSharedMemoryException(string message) : base(message) { }

  public DeadSharedMemoryException(string message, Exception innerException) : base(message, innerException) { }
}
