using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

/**
* Author: Joel McGillivray
*
* Brief summary of page:
* This page is the backend code for the login which validates a users authenticity and logs them in
*/

namespace hobbyshop.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        /// <summary>
        /// Auto scaffolded data for the signing in
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Auto scaffolded data for the signing in
        /// </summary>
        /// <param name="signInManager">The management system for signing in by identity</param>
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Users input
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }
        /// <summary>
        /// The URL they're returned to
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// Error message if there is something wrong with login
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// When the page is loaded they're getting the login page
        /// </summary>
        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        /// <summary>
        /// If the user is validated they are redirected to the main page (index.html) and logged in
        /// </summary>
        /// <returns>If they aren't validated theyre returned the page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, 
                    false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(ReturnUrl);
                }
                else if (result.IsNotAllowed)
                {
                    ErrorMessage = "Login is not allowed.";
                }
                else if (result.IsLockedOut)
                {
                    ErrorMessage = "Account is locked out.";
                }
                else
                {
                    ErrorMessage = "Invalid login attempt.";
                }
            }
            

            return Page();
        }

        public class InputModel
        {
            /// <summary>
            /// In order to login the user needs their email
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// In order to login the user also needs their password
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
