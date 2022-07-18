using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.HM.Interop;

[Serializable]
[PublicAPI]
public struct MAHM_SHARED_MEMORY_GPU_ENTRY {
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
  public char[] gpuId;

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
  public char[] family;

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
  public char[] device;

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
  public char[] driver;

  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
  public char[] bios;

  public uint memAmount;
}
