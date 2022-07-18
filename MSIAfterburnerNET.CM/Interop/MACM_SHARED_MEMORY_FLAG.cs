using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.CM.Interop;

[Flags]
[PublicAPI]
public enum MACM_SHARED_MEMORY_FLAG : uint {
  None = 0,
  Link = 0x00000001,
  Sync = 0x00000002,
  Thermal = 0x00000004,
}
