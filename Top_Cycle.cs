// Top Cycle

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
    //get the bounds of the current window
    Rectangle windowBounds = BFS.Window.GetBounds(windowHandle);

    //get the bounds of the current monitor
    Rectangle monitorBounds = BFS.Monitor.GetMonitorWorkAreaByWindow(windowHandle);

    if (windowBounds.Y != monitorBounds.Y)
    {
      BFS.Window.MoveToTopMonitorHalf(windowHandle);
      return;
    }

    int percent_50 = (int)Math.Floor(monitorBounds.Height * 0.5);
    int percent_33 = (int)Math.Floor(monitorBounds.Height * 0.33);
    int adjustment = monitorBounds.Height - (percent_33 * 3);
    int percent_34 = percent_33 + adjustment;
    int percent_66 = percent_33 * 2;
    int percent_67 = percent_66 + adjustment;

    // Cycle
    if(IsWithin(windowBounds.Height, percent_50, 5)) {
      BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left, monitorBounds.Top, monitorBounds.Width, percent_67);
      return;
    }
    else if(IsWithin(windowBounds.Height, percent_67, 5)) {
      BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left, monitorBounds.Top, monitorBounds.Width, percent_33);
      return;
    }
    else if(IsWithin(windowBounds.Height, percent_33, 5)) {
      BFS.Window.SetSizeAndLocation(windowHandle, monitorBounds.Left, monitorBounds.Top + percent_33, monitorBounds.Width, percent_34);
      return;
    }
    else {
      BFS.Window.MoveToTopMonitorHalf(windowHandle);
      return;
    }
  }

  //this function returns true if a value is close to another value
  private static bool IsWithin(int value, int target, int threshold)
  {
    return (value == target) || ((value >= target - threshold) && (value <= target + threshold));
  }
}