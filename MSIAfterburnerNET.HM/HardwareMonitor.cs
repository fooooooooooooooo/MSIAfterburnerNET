using System;
using JetBrains.Annotations;
using MSIAfterburnerNET.Common;
using MSIAfterburnerNET.Common.Exceptions;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM;

[PublicAPI]
public class HardwareMonitor : IDisposable {
  private HmSharedMemory _mmf;

  public HardwareMonitor() {
    Header = new HardwareMonitorHeader();
    Refresh();
  }

  public static uint GlobalIndex { get; set; } = uint.MaxValue;

  public HardwareMonitorHeader Header { get; }
  public HardwareMonitorEntry[] Entries { get; private set; }
  public HardwareMonitorGpuEntry[] GpuEntries { get; private set; }

  private void LoadEntry(uint index) {
    if (index >= Header.EntryCount) return;

    long offset = Header.HeaderSize + Header.EntrySize * index;

    MAHM_SHARED_MEMORY_ENTRY entry = default;

    _mmf.ReadMahmEntry(ref entry, offset);

    Entries[index].NativeEntry = entry;
  }

  private void LoadGpuEntry(uint gpuIndex) {
    if (gpuIndex >= Header.GpuEntryCount) return;

    long offset = (uint) ((int) Header.HeaderSize +
                          (int) Header.EntrySize * (int) Header.EntryCount +
                          (int) Header.GpuEntrySize * (int) gpuIndex);

    MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry = default;

    _mmf.ReadMahmGpuEntry(ref gpuEntry, offset);

    GpuEntries[gpuIndex].NativeGpuEntry = gpuEntry;
  }

  private void OpenMemory() {
    CloseMemory();

    try {
      _mmf = new HmSharedMemory("MAHMSharedMemory", Win32Api.FileMapAccess.FileMapAllAccess);
    }
    catch (Exception ex) {
      throw new SharedMemoryNotFoundException(ex);
    }
  }

  private void CloseMemory() {
    _mmf?.Dispose();
    _mmf = null;

    GC.Collect();
  }

  public void Refresh() {
    RefreshHeader();

    Entries = new HardwareMonitorEntry[Header.EntryCount];
    for (uint i = 0; i < Entries.Length; ++i) {
      Entries[i] = new HardwareMonitorEntry();
      LoadEntry(i);
    }

    GpuEntries = new HardwareMonitorGpuEntry[Header.GpuEntryCount];
    for (uint i = 0; i < GpuEntries.Length; ++i) {
      GpuEntries[i] = new HardwareMonitorGpuEntry(i);
      LoadGpuEntry(i);
    }
  }

  public void RefreshHeader() {
    OpenMemory();
    MAHM_SHARED_MEMORY_HEADER header = default;
    _mmf.ReadMahmHeader(ref header);
    Header.NativeHeader = header;
    Header.Validate();
  }

  public void RefreshGpuEntry(uint gpuIndex) {
    if (gpuIndex > Header.GpuEntryCount - 1U)
      throw new ArgumentOutOfRangeException(
        nameof(gpuIndex),
        $"{nameof(gpuIndex)} is out of range: {gpuIndex} > {Header.GpuEntryCount - 1U}"
      );

    LoadGpuEntry(gpuIndex);
  }

  public void RefreshEntry(uint index) {
    if (index > Header.EntryCount - 1U)
      throw new ArgumentOutOfRangeException(
        nameof(index),
        $"{nameof(index)} is out of range: {index} > {Header.EntryCount - 1U}"
      );

    LoadEntry(index);
  }

  public void RefreshEntry(MONITORING_SOURCE_ID id) {
    if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
      throw new ArgumentOutOfRangeException(
        nameof(id),
        $"{nameof(id)} is out of range: {id} is not in {nameof(MONITORING_SOURCE_ID)}"
      );

    for (uint i = 0; i < Header.EntryCount; ++i)
      if (Entries[i].SrcId == id)
        LoadEntry(i);
  }

  public void RefreshEntry(MONITORING_SOURCE_ID id, uint componentIndex) {
    if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
      throw new ArgumentOutOfRangeException(
        nameof(id),
        $"{nameof(id)} is out of range: {id} is not in {nameof(MONITORING_SOURCE_ID)}"
      );

    var index = Array.FindIndex(Entries, e => e.SrcId == id && e.ComponentIndex == componentIndex);
    if (index >= 0) LoadEntry((uint) index);
  }

  public void RefreshEntry(string name, uint componentIndex) {
    var index = Array.FindIndex(Entries, e => e.SrcName == name && e.ComponentIndex == componentIndex);
    if (index >= 0) LoadEntry((uint) index);
  }

  public void RefreshEntry(HardwareMonitorEntry dataSource) {
    var index = Array.FindIndex(Entries, e => e == dataSource);
    if (index >= 0) LoadEntry((uint) index);
  }

  public HardwareMonitorEntry GetEntry(MONITORING_SOURCE_ID id) {
    if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
      throw new ArgumentOutOfRangeException(
        nameof(id),
        $"{nameof(id)} is out of range: {id} is not in {nameof(MONITORING_SOURCE_ID)}"
      );

    return Array.Find(Entries, e => e.SrcId == id);
  }

  public HardwareMonitorEntry GetEntry(MONITORING_SOURCE_ID id, uint componentIndex) {
    if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
      throw new ArgumentOutOfRangeException(
        nameof(id),
        $"{nameof(id)} is out of range: {id} is not in {nameof(MONITORING_SOURCE_ID)}"
      );

    return Array.Find(Entries, e => e.SrcId == id && e.ComponentIndex == componentIndex);
  }

  public HardwareMonitorEntry GetEntry(string name, uint componentIndex) {
    return Array.Find(Entries, e => e.SrcName == name && e.ComponentIndex == componentIndex);
  }

  #region IDisposable Support

  private bool _disposedValue; // To detect redundant calls

  protected virtual void Dispose(bool disposing) {
    if (_disposedValue) return;


    CloseMemory();

    _disposedValue = true;
  }

  public void Dispose() {
    Dispose(true);

    GC.SuppressFinalize(this);
  }

  #endregion IDisposable Support
}
