using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, EmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            try
            {

            
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Generate an email verification token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Create the verification link
                    var verificationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    // Send the verification email
                    var emailSubject = "Email Verification";
                    var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
                    _emailService.SendEmail(user.Email, emailSubject, emailBody);

                    // Check if "Admin" role exists
                    var adminRole = await _roleManager.FindByNameAsync("Admin");

                    if (adminRole == null)
                    {
                        // Create the "Admin" role if it doesn't exist
                        adminRole = new IdentityRole("Admin");
                        await _roleManager.CreateAsync(adminRole);
                    }

                    // Add the user to the "Admin" role
                    await _userManager.AddToRoleAsync(user, "Admin");

                    _logger.LogInformation($"User {model.Email} registered successfully");
                    return Ok("Registration successful.");
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during registration for user {model.Email}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Add an action to handle email verification
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok("Email verification successful.");
            }

            return BadRequest("Email verification failed.");
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            try
            {

            
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    var token = GenerateJwtToken(user,roles);
                    _logger.LogInformation($"User {model.Email} logged in successfully");
                    return Ok(new { Message = "Login successful.", Token = token });//Print success message and token if the login was successful
                }

                return Unauthorized("Invalid login attempt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during login for user {model.Email}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            try
            {

            
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogWarning($"User with email {email} not found for deletion");
                    return NotFound("User not found.");
                }

                // Get roles of the user
                var userRoles = await _userManager.GetRolesAsync(user);

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    // Sign out the user if they were signed in
                    await _signInManager.SignOutAsync();

                    // Remove roles associated with the user
                    foreach (var role in userRoles)
                    {
                        var roleResult = await _userManager.RemoveFromRoleAsync(user, role);
                        if (!roleResult.Succeeded)
                        {
                            // Handle the case where removing a role fails
                            return BadRequest($"Failed to remove user from role '{role}'.");
                        }
                    }

                    _logger.LogInformation($"Account for user {email} deleted successfully.");
                    return Ok("Account and associated roles deleted successfully.");
                }

                _logger.LogError($"Failed to delete account for user {email}");
                return BadRequest("Failed to delete account.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during account deletion for user {email}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}