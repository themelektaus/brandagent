using System.Linq;

namespace Brandagent;

public static class Selection
{
    public static Data data;
    public static string activeSecret;

    public static Data.Item ActiveDataItem
        => data?.items.FirstOrDefault(x => x.secret == activeSecret);

    public static string ActiveDataItemCurrentOtp { get; private set; }
    public static string ActiveDataItemNextOtp { get; private set; }
    public static int ActiveDataItemTimer { get; private set; }

    public static void ClearActiveDataItem()
    {
        ActiveDataItemCurrentOtp = null;
        ActiveDataItemNextOtp = null;
        ActiveDataItemTimer = 0;
    }

    public static void ComputeActiveDataItem()
    {
        if (ActiveDataItem is null)
        {
            ClearActiveDataItem();
            return;
        }

        ActiveDataItem.Compute(out var currentOtp, out var nextOtp, out var timer);

        ActiveDataItemCurrentOtp = currentOtp;
        ActiveDataItemNextOtp = data.showNextOtp ? nextOtp : null;
        ActiveDataItemTimer = timer;
    }
}
