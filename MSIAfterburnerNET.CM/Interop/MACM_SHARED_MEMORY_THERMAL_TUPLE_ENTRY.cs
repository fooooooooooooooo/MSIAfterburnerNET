using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.CM.Interop;

[Serializable]
[PublicAPI]
public struct MACM_SHARED_MEMORY_THERMAL_TUPLE_ENTRY {
  public uint temperatureCur;
  public uint temperatureDef;
  public uint frequencyCur;
  public uint frequencyDef;
}
