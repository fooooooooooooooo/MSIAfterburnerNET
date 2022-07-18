using JetBrains.Annotations;
using MSIAfterburnerNET.CM.Interop;

namespace MSIAfterburnerNET.CM;

[PublicAPI]
internal class ControlMemoryGpuEntry {
  private int _index = -1;
  private bool _isMaster;

  public ControlMemoryGpuEntry() { }

  public ControlMemoryGpuEntry(MACM_SHARED_MEMORY_GPU_ENTRY gpuEntry) { NativeGpuEntry = gpuEntry; }

  public ControlMemoryGpuEntry(ControlMemoryHeader header, int index) {
    _index = index;
    _isMaster = index == header.MasterGpu;
  }

  public MACM_SHARED_MEMORY_GPU_ENTRY? NativeGpuEntry { get; set; }
}
