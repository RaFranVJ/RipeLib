using System;
using System.Runtime.InteropServices;

public static class PlatformHelper
{
public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

public static bool IsMacOS => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}