using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.Common.Exceptions;

[PublicAPI]
public class UnsupportedSharedMemoryVersionException : Exception {
  private const string UnsupportedMessage = "Connected to an unsupported version of MSI Afterburner shared memory.";

  public UnsupportedSharedMemoryVersionException() : base(UnsupportedMessage) { }

  public UnsupportedSharedMemoryVersionException(Exception innerException) : base(UnsupportedMessage, innerException) { }

  public UnsupportedSharedMemoryVersionException(string message) : base(message) { }

  public UnsupportedSharedMemoryVersionException(string message, Exception innerException) : base(message, innerException) { }
}
