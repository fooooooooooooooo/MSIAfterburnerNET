using JetBrains.Annotations;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM;

[PublicAPI]
public class HardwareMonitorGpuEntry {
  public HardwareMonitorGpuEntry() { }

  public HardwareMonitorGpuEntry(uint index) { Index = index; }

  public HardwareMonitorGpuEntry(MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry) : this() { NativeGpuEntry = gpuEntry; }

  public HardwareMonitorGpuEntry(MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry, uint index) : this(index) { NativeGpuEntry = gpuEntry; }

  public MAHM_SHARED_MEMORY_GPU_ENTRY? NativeGpuEntry { get; set; }
  public uint Index { get; } = HardwareMonitor.GlobalIndex;

  public string GpuId => NativeGpuEntry.HasValue ? new string(NativeGpuEntry.Value.gpuId).TrimEnd((char) 0) : null;

  public string Family => NativeGpuEntry.HasValue ? new string(NativeGpuEntry.Value.family).TrimEnd((char) 0) : null;

  public string Device => NativeGpuEntry.HasValue ? new string(NativeGpuEntry.Value.device).TrimEnd((char) 0) : null;

  public string Driver => NativeGpuEntry.HasValue ? new string(NativeGpuEntry.Value.driver).TrimEnd((char) 0) : null;

  public string BIOS => NativeGpuEntry.HasValue ? new string(NativeGpuEntry.Value.bios).TrimEnd((char) 0) : null;

  public uint MemAmount => NativeGpuEntry?.memAmount ?? 0;

  public override string ToString() {
    try {
      return "GpuId = " +
             GpuId +
             ";Family = " +
             Family +
             ";Device = " +
             Device +
             ";Driver = " +
             Driver +
             ";BIOS = " +
             BIOS +
             ";MemAmount = " +
             MemAmount;
    }
    catch {
      return base.ToString();
    }
  }
}
