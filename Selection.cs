using System.Linq;

namespace Brandagent;

public static class Selection
{
    public static Data data;
    public static string activeSecret;

    public static Data.Item ActiveDataItem
        => data?.items.FirstOrDefault(x => x.secret == activeSecret);

    public static Data.Item.Context ActiveDataItemContext { get; private set; }
    
    public static void ClearActiveDataItem()
    {
        ActiveDataItemContext = default;
    }

    public static void ComputeActiveDataItem()
    {
        if (ActiveDataItem is null)
        {
            ClearActiveDataItem();
            return;
        }

        ActiveDataItem.Compute(out var context);
        ActiveDataItemContext = context;
    }
}
