using System.Linq;

namespace Brandagent;

public static class Selection
{
    public static Data data;
    public static string activeSecret;

    public static Data.Item ActiveDataItem
        => data?.items.FirstOrDefault(x => x.secret == activeSecret);

    public static string ActiveDataItemOtp { get; private set; }
    public static int ActiveDataItemTimer { get; private set; }

    public static void ComputeActiveDataItem()
    {
        if (ActiveDataItem is null)
        {
            ActiveDataItemOtp = null;
            ActiveDataItemTimer = 0;
            return;
        }

        ActiveDataItem.Compute(out var otp, out var timer);
        ActiveDataItemOtp = otp;
        ActiveDataItemTimer = timer;
    }
}
