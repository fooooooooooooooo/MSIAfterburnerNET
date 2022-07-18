using System;
using JetBrains.Annotations;

namespace MSIAfterburnerNET.CM.Interop;

[Flags]
[PublicAPI]
public enum MACM_SHARED_MEMORY_GPU_ENTRY_FLAG : uint {
  None = 0,
  CoreClock = 0x00000001,
  ShaderClock = 0x00000002,
  MemoryClock = 0x00000004,
  FanSpeed = 0x00000008,
  CoreVoltage = 0x00000010,
  MemoryVoltage = 0x00000020,
  AuxVoltage = 0x00000040,
  CoreVoltageBoost = 0x00000080,
  MemoryVoltageBoost = 0x00000100,
  AuxVoltageBoost = 0x00000200,
  PowerLimit = 0x00000400,
  CoreClockBoost = 0x00000800,
  MemoryClockBoost = 0x00001000,
  ThermalLimit = 0x00002000,
  ThermalPrioritize = 0x00004000,
  Aux2Voltage = 0x00008000,
  Aux2VoltageBoost = 0x00010000,
  VfCurve = 0x00020000,
  VfCurveEnabled = 0x00040000,
  SynchronizedWithMaster = 0x80000000,
}
