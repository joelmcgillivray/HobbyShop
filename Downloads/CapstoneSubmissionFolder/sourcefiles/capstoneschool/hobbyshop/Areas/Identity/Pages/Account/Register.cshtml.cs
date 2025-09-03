using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

/**
* Author: Joel McGillivray
*
* Brief summary of page:
* This is the backend code for the register page, which handles validation and creation of a user
*/

namespace hobbyshop.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        /// <summary>
        /// To be able to sign in a user immedietly after account validation/creation
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;
        /// <summary>
        /// Handles the management of the user
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        ///  To create identity roles
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;
        [BindProperty]
        public InputModel Input { get; set; }
        /// <summary>
        /// Where to return the user, which in this case will be the base page (index.html)
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// The required set up to create a user 
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Setting the email of the user also what they use to sign up 
            /// Required email, and max of 100 string length
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "Please enter a valid email address")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            /// <summary>
            /// Password for the user account 
            /// String length max is 100, and minimum 8
            /// Must also have atleast 1 char, 1 number, 1 uppercase, and 1 lowercase and 1 special character
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,100}$", ErrorMessage = "The password must contain at least 1 numeric character, 1 uppercase character, and 1 special character.")]
            public string Password { get; set; }
        }

        /// <summary>
        /// Registering the user, assigning role, and signing them in on creation of user
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        public RegisterModel(RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// When initialized it will create the user roles, admin and regular user
        /// </summary>
        /// <returns>Returns the user to the URL</returns>
        public async Task OnGet()
        {
            // Create 'user' role if it doesn't exist
           if (!await _roleManager.RoleExistsAsync("user"))
            {
                var role = new IdentityRole
                {
                    Name = "user"
                };
                await _roleManager.CreateAsync(role);
            }

            // Create 'admin' role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                var role = new IdentityRole
                {
                    Name = "admin"
                };
                await _roleManager.CreateAsync(role);
            }
            ReturnUrl = Url.Content("~/");
        }

        /// <summary>
        /// When initialized it will create the first user as an admin, and all other users as regular users
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {

                // Check if the email is already in use
                var existingUser = await _userManager.FindByEmailAsync(Input.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return Page();
                }

                // If new user
                var identity = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(identity, Input.Password);

                if (result.Succeeded)
                {
                    // Check if this is the first user
                    if (!_userManager.Users.Any())
                    {
                        // Assign 'admin' role to the first user
                        await _userManager.AddToRoleAsync(identity, "admin");
                    }
                    else
                    {
                        // Assign 'user' role to all other users
                       await _userManager.AddToRoleAsync(identity, "user");
                    }

                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
            }
            return Page();
        }
    }
}
