using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.HM.Interop;

[Flags]
[PublicAPI]
public enum MAHM_SHARED_MEMORY_ENTRY_FLAG : uint {
  None = 0,
  ShowInOsd = 0x00000001,
  ShowInLcd = 0x00000002,
  ShowInTray = 0x00000004,
}
