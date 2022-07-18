using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.CM.Interop;

[Flags]
[PublicAPI]
public enum MACM_SHARED_MEMORY_COMMAND : uint {
  None = 0,
  Init = 0x00AB0000,
  Flush = 0x00AB0001,
  FlushWithoutApplying = 0x00AB0002,
  RefreshVfCurve = 0x00AB0003,
}
