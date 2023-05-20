// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Identity.Pages.Account.Manage;

public class PersonalDataModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<PersonalDataModel> _logger;

    public PersonalDataModel(
        UserManager<ApplicationUser> userManager,
        ILogger<PersonalDataModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            var userId = _userManager.GetUserId(User);
            _logger.LogError("Unable to load user with ID: {UserId}", userId);
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        _logger.LogInformation("User '{UserName}' accessed the PersonalData page.", user.UserName);
        return Page();
    }
}
