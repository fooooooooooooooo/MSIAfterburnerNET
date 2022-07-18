using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MSIAfterburnerNET.Common;
using MSIAfterburnerNET.Common.SharedMemory;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM;

internal class HmSharedMemory : ReadonlyMemory {
  public HmSharedMemory(string name, Win32Api.FileMapAccess accessLevel) : base(name, accessLevel) { }

  public void ReadMahmHeader(ref MAHM_SHARED_MEMORY_HEADER header) {
    var ptr = IntPtr.Zero;
    try {
      ptr = Win32Api.MapViewOfFile(
        mmfHandle,
        Win32Api.FileMapAccess.FileMapRead,
        0,
        Marshal.SizeOf((object) header)
      );

      if (ptr == IntPtr.Zero) throw new Win32Exception();

      var structure = Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_HEADER));

      header = (MAHM_SHARED_MEMORY_HEADER?) structure ?? throw new Win32Exception();
    }
    finally {
      if (ptr != IntPtr.Zero) Win32Api.UnmapViewOfFile(ptr);
    }
  }

  public void ReadMahmEntry(ref MAHM_SHARED_MEMORY_ENTRY entry, long offset) {
    var baseAddress = IntPtr.Zero;
    try {
      long entrySize = Marshal.SizeOf((object) entry);
      var ddFileOffset = offset / allocationGranularity * allocationGranularity;
      offset -= ddFileOffset;

      baseAddress = Win32Api.MapViewOfFile(
        mmfHandle,
        Win32Api.FileMapAccess.FileMapRead,
        ddFileOffset,
        Convert.ToInt32(offset + entrySize)
      );
      if (baseAddress == IntPtr.Zero) throw new Win32Exception();

      var ptr = new IntPtr(baseAddress.ToInt64() + offset);

      var structure = Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_ENTRY));

      entry = (MAHM_SHARED_MEMORY_ENTRY?) structure ?? throw new Win32Exception();
    }
    finally {
      if (baseAddress != IntPtr.Zero) Win32Api.UnmapViewOfFile(baseAddress);
    }
  }

  public void ReadMahmGpuEntry(ref MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry, long offset) {
    var lpBaseAddress = IntPtr.Zero;
    try {
      long entrySize = Marshal.SizeOf((object) gpuEntry);
      var ddFileOffset = offset / allocationGranularity * allocationGranularity;
      offset -= ddFileOffset;

      lpBaseAddress = Win32Api.MapViewOfFile(
        mmfHandle,
        Win32Api.FileMapAccess.FileMapRead,
        ddFileOffset,
        Convert.ToInt32(offset + entrySize)
      );
      if (lpBaseAddress == IntPtr.Zero) throw new Win32Exception();

      var ptr = new IntPtr(lpBaseAddress.ToInt64() + offset);

      var structure = Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_GPU_ENTRY));

      gpuEntry = (MAHM_SHARED_MEMORY_GPU_ENTRY?) structure ?? throw new Win32Exception();
    }
    finally {
      if (lpBaseAddress != IntPtr.Zero) Win32Api.UnmapViewOfFile(lpBaseAddress);
    }
  }
}
