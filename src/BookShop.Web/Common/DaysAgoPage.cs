namespace BookShop.Web.Common;

public abstract class DaysAgoPage: Microsoft.AspNetCore.Mvc.RazorPages.Page
{
    public int DaysAgo(DateTime timeCreated)
    {
        return (DateTime.Now - timeCreated).Days;
    }
}
