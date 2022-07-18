using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.CM.Interop;

[Flags]
[PublicAPI]
public enum MACM_SHARED_MEMORY_GPU_ENTRY_FAN_FLAG : uint { None = 0, Auto = 1 }
