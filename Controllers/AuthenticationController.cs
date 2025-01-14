﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using RedBusService.Requests;
namespace RedBusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        IConfiguration configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterRequest request)
        {
            if (!roleManager.RoleExistsAsync(UserRoles.Admin).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).GetAwaiter().GetResult();
            if (!roleManager.RoleExistsAsync(UserRoles.User).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole(UserRoles.User)).GetAwaiter().GetResult();

            if (!roleManager.RoleExistsAsync(UserRoles.Driver).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole(UserRoles.Driver)).GetAwaiter().GetResult();
            


            IdentityUser user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
            };

            var result = userManager.CreateAsync(user, request.Password).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, request.ChooseRole.ToString()).GetAwaiter().GetResult();
                //var token = GenerateJwtToken(user);
                return Ok(user);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var user = userManager.FindByEmailAsync(request.Email).GetAwaiter().GetResult();
            if (user == null)
                return BadRequest("User not found");
            var result = userManager.CheckPasswordAsync(user, request.Password).GetAwaiter().GetResult();
            if (!result)
                return BadRequest("Password incorrect");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        string GenerateJwtToken(IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JWTSecrets"]);

            var identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"));

            foreach (var item in userManager.GetRolesAsync(user).Result)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, item));
            }

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            //var expiryTimeSpan = TimeSpan.FromDays(7);
            var expiry = DateTime.UtcNow.AddDays(7);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512),
                Subject = identity,
                Expires = expiry,
            });

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }
}
