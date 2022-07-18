using System;
using System.Threading;
using MSIAfterburnerNET.HM;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.Sample;

internal static class Program {
  private static void Main() {
    try {
      using var mahm = new HardwareMonitor();

      // print out current MACM Header values
      Console.WriteLine("***** MSI AFTERBURNER HARDWARE MONITOR HEADER *****");
      Console.WriteLine(mahm.Header.ToString().Replace(";", "\n"));
      Console.WriteLine();

      // print out current Entry values
      for (var i = 0; i < mahm.Header.EntryCount; ++i) {
        Console.WriteLine($"***** MSI AFTERBURNER DATA SOURCE {i} *****");
        Console.WriteLine(mahm.Entries[i].ToString().Replace(";", "\n"));
        Console.WriteLine();
      }

      // print out current MAHM GPU Entry values
      for (var i = 0; i < mahm.Header.GpuEntryCount; ++i) {
        Console.WriteLine($"***** MSI AFTERBURNER GPU {i} *****");
        Console.WriteLine(mahm.GpuEntries[i].ToString().Replace(";", "\n"));
        Console.WriteLine();
      }

      // show a data source monitor several times
      var framerate = mahm.GetEntry(MONITORING_SOURCE_ID.Framerate, HardwareMonitor.GlobalIndex);
      if (framerate == null) return;

      Console.WriteLine("***** FRAMERATE *****");

      for (var i = 0; i < 10; ++i) {
        Console.WriteLine(framerate.Data);
        Thread.Sleep(1000);
        mahm.RefreshEntry(framerate);
      }
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);
    }
  }
}
