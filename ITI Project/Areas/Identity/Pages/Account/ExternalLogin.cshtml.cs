#nullable disable
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ITI_Project.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
        }

        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");

            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // جلب الايميل من Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                ErrorMessage = "Email not received from Google.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // التحقق إذا المستخدم موجود
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // لو موجود: ربط الحساب الخارجي لو مش مربوط مسبقًا
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (!result.Succeeded)
                {
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", email, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            // لو مش موجود: إنشاء حساب جديد تلقائيًا
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("New user created with {Name} provider.", info.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            // لو فيه أي خطأ في إنشاء الحساب
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToPage("./Login");
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
