using System;
using System.IO;

namespace Brandagent;

public static class Utils
{
    public static string GetDownloadFolder()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads"
        );
    }
}
