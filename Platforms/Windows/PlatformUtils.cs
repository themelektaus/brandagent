using Microsoft.Win32;

namespace Brandagent;

public static class PlatformUtils
{
    public static string GetDownloadFolder()
    {
        return Registry.GetValue(
            @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders",
            "{374DE290-123F-4565-9164-39C4925E467B}",
            string.Empty
        ).ToString();
    }
}
