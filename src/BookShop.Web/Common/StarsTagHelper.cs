using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookShop.Web.Common;

public class StarsTagHelper:TagHelper
{
    const string starEmpty = """
                               <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAACaUlEQVR4nL1W3WoTQRidBLEgYkzSnZldakUJXlRsc+dFay7EGxVRK16paKyg4EPkfXyAUqiw7MwkVehlr3wIG6NoZ2fhk7M2kmpis1uzHwzZ7HznnO/M7zKWM4hYGY0VHdYETWu8ZqGiRKycaLGBVqhra7xmrP01NKu9lUJEOx1WtlpsfNpszO29WzpdmGurvZXY+DeG//FsFV8uzC07DNorwLVVfHnU7TDiSLZm5rozxm0hri3cRrI1qT/uypbt8Wu5BShkp2iHiwMllzCszvD7NhIvEi1e0hi3v3GbjTnkIBcYYMEBLnD+7cR4TdcVd62Sz6wSbfw6I+/ESlynntfoby3UMMzTFo5cYIAFB7iOcEMLJ963aN5PtHiLZDbj6G8t1KAFzfTFQEnPRvxVXy9WZyX6ebtaSRfgDhdHOij051PxGTin3V+iX9//ITqMwaE49f6fOIUXz6eiXcGPTUzFlfROLPoxqIPry3ZQnwqwH1bSKum4Kv8Rg7xTtx9WUudYFFlFgQEWHCxPOMUf5FlswADL8oZVok3ESllxwOAEyyVKxEqJyQk+QdGM9GLVaf9hXmFgsUOyC6vgSqz81Un9OAwmHgi4rZS/Co7MwrH21w7GALEnnZbriZaP0fA8bp8CC47Mwk7L9dHtQB9q55yWt63xn5CpB8P3cO2MfJQO7chZj2HONVVJV7QZYyXa9c84zW9ZLZ//iMSlSfnfo+BCYuRTp8U9Cr2zwGZenJ0OK8davnZdcRNg3K3TYpGLFQ0sOLLc5QxzhnvTanEVlWeqeliA5pcTI99kunDwoZ6p0gkBDnCN6/wJIPqAgQeQvuYAAAAASUVORK5CYII=">
                             """;
    const string starFilled = """
                                <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAACXBIWXMAAAsTAAALEwEAmpwYAAABtElEQVR4nO2Vu0oDURCG1wsiagRBBAsRonEmihb6AKL4Cj6GqCh4a8RKLARBQbGws0hlL5rCBDPjFhZpbYN3sBDBwl92o8Ekq2bPJinEgR+2ODPf/LO7cyzrP0oMpHpbHVnVDgitQGm5utDLoWYI30LpHgkKVQ8svAhluBJeqJ5b5Zsv4Duk+1sqD1aaz0FzcJqrLDTe3QihTBFY6Rr2SFPlwBc8WwzNaab8wASFYPOwt9vcuDPuGb9fOdL9DdBIGBqdgNI0hPcgdAzhKwi//eDUQ/QIZRvKMQitQmgSKR4pagqwaiF06K+4gYQOHVY+PGbVQfmgotD4aL33uGHVQGi7/GDaL3LqCVfeKp9T3v0VWuB8swzj3XFqlQTNa0BpLYDTDd/ArwHhIwPokRU0oHxq4PgkOFice9e347tg0GS4w/gdJ8Md5uBU37gxOEVj5mChqR928VJW7rPXrzQVAMx7BcWeobSOs8G2vJtLeAHKT4WLIwg4+QF8dZuwufPbs3Zfu9uU8ssHOGEGhbM63THGYPf2lJynA13ZSdGD2daKdzfiPBL1nfiZfx6JOjVM860/F+/E86pEkuilkgAAAABJRU5ErkJggg==">
                             """;
    public int Count { get; set; }

    public int Total { get; set; } = 5;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {

        for (int i = 0; i < Total; i++)
        {
            if (i<Count)
            {
                output.Content.AppendHtml(starFilled);
            }
            else
            {
                output.Content.AppendHtml(starEmpty);
            }
        }
    }
}
