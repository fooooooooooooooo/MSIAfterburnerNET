using JetBrains.Annotations;
using MSIAfterburnerNET.Common.Extensions;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM;

[PublicAPI]
public class HardwareMonitorEntry {
  public HardwareMonitorEntry() { }

  public HardwareMonitorEntry(MAHM_SHARED_MEMORY_ENTRY entry) { NativeEntry = entry; }
  public MAHM_SHARED_MEMORY_ENTRY? NativeEntry { get; set; }

  public string SrcName => NativeEntry.HasValue ? new string(NativeEntry.Value.srcName).TrimEnd((char) 0) : null;

  public string SrcUnits => NativeEntry.HasValue ? new string(NativeEntry.Value.srcUnits).TrimEnd((char) 0) : null;

  public string LocalizedSrcName
    => NativeEntry.HasValue ? new string(NativeEntry.Value.localizedSrcName).TrimEnd((char) 0) : null;

  public string LocalizedSrcUnits
    => NativeEntry.HasValue ? new string(NativeEntry.Value.localizedSrcUnits).TrimEnd((char) 0) : null;

  public string RecommendedFormat
    => NativeEntry.HasValue ? new string(NativeEntry.Value.recommendedFormat).TrimEnd((char) 0) : null;

  public float Data
    => NativeEntry.HasValue && !NativeEntry.Value.data.IsAlmostEqual(float.MaxValue, 1E-20f)
      ? NativeEntry.Value.data
      : 0.0f;

  public float MinLimit => NativeEntry?.minLimit ?? 0.0f;
  public float MaxLimit => NativeEntry?.maxLimit ?? 0.0f;
  public MAHM_SHARED_MEMORY_ENTRY_FLAG Flags => NativeEntry?.flags ?? MAHM_SHARED_MEMORY_ENTRY_FLAG.None;
  public uint ComponentIndex => NativeEntry?.index ?? HardwareMonitor.GlobalIndex;

  public MONITORING_SOURCE_ID SrcId
    => NativeEntry.HasValue ? (MONITORING_SOURCE_ID) NativeEntry.Value.srcId : MONITORING_SOURCE_ID.Unknown;

  public override string ToString() {
    try {
      return "SrcName = " +
             SrcName +
             ";SrcUnits = " +
             SrcUnits +
             ";LocalizedSourceName = " +
             LocalizedSrcName +
             ";LocalizedSrcUnits = " +
             LocalizedSrcUnits +
             ";RecommendedFormat = " +
             RecommendedFormat +
             ";Data = " +
             Data +
             ";MinLimit = " +
             MinLimit +
             ";MaxLimit = " +
             MaxLimit +
             ";Flags = " +
             Flags +
             ";ComponentIndex = " +
             ComponentIndex +
             ";SrcId = " +
             SrcId;
    }
    catch {
      return base.ToString();
    }
  }
}
