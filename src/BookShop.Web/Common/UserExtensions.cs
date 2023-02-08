using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BookShop.Web.Common;

public static class UserExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is not null)
            {
                var userId = userIdClaim.Value;
                return userId;
            }
            return string.Empty;
        }

        return string.Empty;

    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        if (user is not null && user.Identity is not null && user.Identity.IsAuthenticated)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return user.Identity.Name;
#pragma warning restore CS8603 // Possible null reference return.
        }

        return string.Empty;
    }
}
