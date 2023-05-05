using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Files;

public class UploadModel : PageModel
{
    private readonly IWebHostEnvironment _env;

    public UploadModel(IWebHostEnvironment env)
    {
        _env = env;
    }

    [BindProperty]
    public IFormFile ContentFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (ContentFile != null)
        {
            using var ms1 = new MemoryStream();
            ContentFile.CopyTo(ms1);
            ms1.Position = 0;

            var path = Path.Combine(_env.ContentRootPath, "Files", ContentFile.FileName);

            using var stream = System.IO.File.Create(path);
            stream.Position = 0;
            await ContentFile.CopyToAsync(stream);
        }

        return RedirectToPage("./Index");
    }
}
