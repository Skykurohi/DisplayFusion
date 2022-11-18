// Center Window

using System;
using System.Drawing;

// The 'windowHandle' parameter will contain the window handle for the:
//   - Active window when run by hotkey
//   - Window Location target when run by a Window Location rule
//   - TitleBar Button owner when run by a TitleBar Button
//   - Jump List owner when run from a Taskbar Jump List
//   - Currently focused window if none of these match
public static class DisplayFusionFunction
{
  public static void Run(IntPtr windowHandle)
  {
    Rectangle windowBounds  = BFS.Window.GetBounds(windowHandle);
    Rectangle monitorBounds = BFS.Monitor.GetMonitorWorkAreaByWindow(windowHandle);
    Rectangle monitorRect   = BFS.Monitor.GetMonitorBoundsByWindow(windowHandle);

    /*if (IsWithin(windowBounds.Width, (int)(monitorBounds.Width * 0.30f), 10)) {
            BFS.Window.Maximize(windowHandle);
    }
    else if(IsWithin(windowBounds.Width, monitorBounds.Width, 10) && !BFS.Window.IsMaximized(windowHandle))
      BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left + (int)(monitorBounds.Width * 0.35), monitorBounds.Top, (int)(monitorBounds.Width * 0.30), monitorBounds.Height);
    else {
            BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left, monitorBounds.Top, monitorBounds.Width, monitorBounds.Height);
    }*/

    if (IsWithin(windowBounds.Width, monitorBounds.Width, 10) && !BFS.Window.IsMaximized(windowHandle)) {
      BFS.Window.Maximize(windowHandle);
      return;
    }
    else {
      BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left, monitorBounds.Top, monitorBounds.Width, monitorBounds.Height);
      return;
    }
  }
  //this function returns true if a value is close to another value
  private static bool IsWithin(int value, int target, int threshold)
  {
    return (value == target) || ((value >= target - threshold) && (value <= target + threshold));
  }
}