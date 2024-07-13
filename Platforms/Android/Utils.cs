using System.IO;

namespace Brandagent;

public static class Utils
{
    public static string GetDownloadFolder()
    {
        return Path.Combine(
            Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
            "Download"
        );
    }
}
