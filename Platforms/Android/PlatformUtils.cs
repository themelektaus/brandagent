using System.IO;

namespace Brandagent;

public static class PlatformUtils
{
    public static string GetDownloadFolder()
    {
        return Path.Combine(
            Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
            "Download"
        );
    }
}
