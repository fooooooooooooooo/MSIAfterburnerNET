﻿using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.HM.Interop;

[Serializable]
[PublicAPI]
public struct MAHM_SHARED_MEMORY_HEADER {
  public uint signature;
  public uint version;
  public uint headerSize;
  public uint entryCount;
  public uint entrySize;
  public uint time;
  public uint gpuEntryCount;
  public uint gpuEntrySize;
}
