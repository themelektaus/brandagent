using System.Linq;

namespace Brandagent;

public static class Selection
{
    public static Data data;
    public static string activeSecret;

    public static Data.Item ActiveDataItem
        => data?.items.FirstOrDefault(x => x.secret == activeSecret);

    public static string[] ActiveDataItemOtps { get; private set; }
    public static int ActiveDataItemTimer { get; private set; }

    public static void ClearActiveDataItem()
    {
        ActiveDataItemOtps = [];
        ActiveDataItemTimer = 0;
    }

    public static void ComputeActiveDataItem()
    {
        if (ActiveDataItem is null)
        {
            ClearActiveDataItem();
            return;
        }

        ActiveDataItem.Compute(out var otps, out var timer);
        ActiveDataItemOtps = otps;
        ActiveDataItemTimer = timer;
    }
}
