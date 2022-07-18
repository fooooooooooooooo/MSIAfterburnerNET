using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.Common.SharedMemory;

[PublicAPI]
public class ReadonlyMemory : IReadableMemory {
  protected readonly uint allocationGranularity;
  protected IntPtr mmfHandle;

  public ReadonlyMemory(string name, Win32Api.FileMapAccess accessLevel) {
    mmfHandle = Win32Api.OpenFileMapping(accessLevel, false, name);

    if (mmfHandle == IntPtr.Zero) throw new Win32Exception();

    var lpSystemInfo = new Win32Api.SYSTEM_INFO();

    Win32Api.GetSystemInfo(ref lpSystemInfo);

    allocationGranularity = lpSystemInfo.dwAllocationGranularity;
  }

  #region IDisposable Support

  private bool _disposedValue; // To detect redundant calls

  protected virtual void Dispose(bool disposing) {
    if (_disposedValue) return;

    if (mmfHandle != IntPtr.Zero) Win32Api.CloseHandle(mmfHandle);

    mmfHandle = IntPtr.Zero;

    _disposedValue = true;
  }

  public void Dispose() {
    Dispose(true);

    GC.SuppressFinalize(this);
  }

  #endregion IDisposable Support
}
