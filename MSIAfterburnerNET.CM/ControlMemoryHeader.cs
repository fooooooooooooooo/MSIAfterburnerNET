using System;
using System.Text;
using JetBrains.Annotations;
using MSIAfterburnerNET.CM.Interop;
using MSIAfterburnerNET.Common.Exceptions;

namespace MSIAfterburnerNET.CM;

[PublicAPI]
public class ControlMemoryHeader {
  public MACM_SHARED_MEMORY_HEADER nativeHeader;

  public ControlMemoryHeader() { }

  public ControlMemoryHeader(MACM_SHARED_MEMORY_HEADER header) { nativeHeader = header; }

  public uint Signature => nativeHeader.signature;
  public uint Version => nativeHeader.version;
  public uint HeaderSize => nativeHeader.headerSize;
  public uint GpuEntryCount => nativeHeader.gpuEntryCount;
  public uint GpuEntrySize => nativeHeader.gpuEntrySize;
  public uint MasterGpu => nativeHeader.masterGpu;
  public MACM_SHARED_MEMORY_FLAG Flags => nativeHeader.flags;
  public DateTime Time => new DateTime(1970, 1, 1).AddSeconds(nativeHeader.time).ToLocalTime();

  public MACM_SHARED_MEMORY_COMMAND Command { get => nativeHeader.command; internal set => nativeHeader.command = value; }

  public string GetSignatureString() {
    var charArray = Encoding.ASCII.GetString(BitConverter.GetBytes(Signature)).ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
  }

  public string GetVersionString() { return $"{Version >> 16}.{(short) Version}"; }

  internal void Validate() {
    if (GetSignatureString() != "MACM") {
      if (Signature == 57005U) throw new DeadSharedMemoryException();

      throw new InvalidSharedMemoryException();
    }

    if (Version < 131073U) throw new UnsupportedSharedMemoryVersionException();
  }

  public override string ToString() {
    try {
      return "Signature = " +
             GetSignatureString() +
             ";Version = " +
             GetVersionString() +
             ";HeaderSize = " +
             HeaderSize +
             ";GpuEntryCount = " +
             GpuEntryCount +
             ";GpuEntrySize = " +
             GpuEntrySize +
             ";MasterGpu = " +
             MasterGpu +
             ";Flags = " +
             Flags +
             ";Time = " +
             Time.ToString("hh:mm:ss MMM-dd-yyyy") +
             ";Command = " +
             Command;
    }
    catch {
      return base.ToString();
    }
  }
}
