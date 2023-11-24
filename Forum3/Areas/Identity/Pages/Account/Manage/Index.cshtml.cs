// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum3.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(32, ErrorMessage = "The {0} must be atleast {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Username")]
            public string UserName { get; set; }
            
            public IFormFile Avatar { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Input = new InputModel
            {
                UserName = userName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (user.UserName != Input.UserName)
            {
                var usernameExists = await _userManager.FindByNameAsync(Input.UserName);
                if (usernameExists != null)
                {
                    ModelState.AddModelError(string.Empty, "Username already exists.");
                    return Page();
                }
                
                user.UserName = Input.UserName;
            }

            // Check if Avatar is changed
            if (Input.Avatar is { Length: > 0 })
            {
                // Ensure avatar is an image
                var isImage = Input.Avatar.ContentType.StartsWith("image/");
                if (!isImage)
                {
                    ModelState.AddModelError(string.Empty, "The avatar must be an image.");
                    return Page();
                }
                
                // Ensure avatar is not too large
                var isTooLarge = Input.Avatar.Length > 1024 * 1024 * 2;
                if (isTooLarge)
                {
                    ModelState.AddModelError(string.Empty, "The avatar must be less than 2 MB.");
                    return Page();
                }
                
                // Get file extension
                var extension = Input.Avatar.FileName.Split('.').Last().ToLower();
                
                // Save avatar to disk
                var avatarFileName = $"{Guid.NewGuid()}.{extension}";
                var avatarPath = $"wwwroot/avatars/{avatarFileName}";
                await using (var stream = System.IO.File.Create(avatarPath))
                {
                    await Input.Avatar.CopyToAsync(stream);
                }
                    
                // Update user avatar
                user.Avatar = avatarFileName;
            }
            
            // Update user
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
