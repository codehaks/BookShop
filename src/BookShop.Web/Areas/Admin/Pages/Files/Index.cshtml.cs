using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Admin.Pages.Files;

public class IndexModel : PageModel
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public IndexModel(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public IList<FileInfo> FileList { get; set; }

    public void OnGet()
    {
        FileList = new List<FileInfo>();
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "/var/lib/data");
        var files = System.IO.Directory.GetFiles(path);
        foreach (var file in files)
        {
            FileList.Add(new FileInfo(file));
        }
    }

    public IActionResult OnGetDownload(string fileName)
    {
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", fileName);
        var content = System.IO.File.ReadAllBytes(path);
        return File(content, "application/pdf", fileName);
    }
}
