using System;
using System.Text;
using JetBrains.Annotations;
using MSIAfterburnerNET.Common.Exceptions;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM;

[PublicAPI]
public class HardwareMonitorHeader {
  public HardwareMonitorHeader() { }

  public HardwareMonitorHeader(MAHM_SHARED_MEMORY_HEADER header) { NativeHeader = header; }
  public MAHM_SHARED_MEMORY_HEADER? NativeHeader { get; set; }

  public uint Signature => NativeHeader?.signature ?? 0;
  public uint Version => NativeHeader?.version ?? 0;
  public uint HeaderSize => NativeHeader?.headerSize ?? 0;
  public uint EntryCount => NativeHeader?.entryCount ?? 0;
  public uint EntrySize => NativeHeader?.entrySize ?? 0;
  public DateTime Time => new DateTime(1970, 1, 1).AddSeconds(NativeHeader?.time ?? 0).ToLocalTime();
  public uint GpuEntryCount => NativeHeader?.gpuEntryCount ?? 0;
  public uint GpuEntrySize => NativeHeader?.gpuEntrySize ?? 0;

  public string GetSignatureString() {
    var charArray = Encoding.ASCII.GetString(BitConverter.GetBytes(Signature)).ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
  }

  public string GetVersionString() { return $"{Version >> 16}.{(short) Version}"; }

  public void Validate() {
    if (GetSignatureString() != "MAHM") {
      if (Signature == 57005U) throw new DeadSharedMemoryException();

      throw new InvalidSharedMemoryException();
    }

    if (Version < 131072U) throw new UnsupportedSharedMemoryVersionException();
  }

  public override string ToString() {
    try {
      return "Signature = " +
             GetSignatureString() +
             ";Version = " +
             GetVersionString() +
             ";HeaderSize = " +
             HeaderSize +
             ";EntryCount = " +
             EntryCount +
             ";EntrySize = " +
             EntrySize +
             ";Time = " +
             Time.ToString("hh:mm:ss MMM-dd-yyyy") +
             ";GpuEntryCount = " +
             GpuEntryCount +
             ";GpuEntrySize = " +
             GpuEntrySize;
    }
    catch {
      return base.ToString();
    }
  }
}
