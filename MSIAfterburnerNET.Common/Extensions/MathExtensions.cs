using System;

namespace MSIAfterburnerNET.Common.Extensions;

public static class MathExtensions {
  public static bool IsAlmostEqual(this float a, float b, float epsilon) { return Math.Abs(a - b) < epsilon; }
}
