using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesafioQSTI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DesafioQSTI.Controllers
{
    public class AuthenticationController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public async Task<IActionResult> CreateAccount(ClienteViewModel clienteViewModel)
        {
            var user = new IdentityUser()
            {
                UserName = clienteViewModel.Nome,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, clienteViewModel.Senha);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, new[] {"admin"} );
            }

            //return Ok(new {Username = user.UserName});
            return View("Index", clienteViewModel);
        }

        public async Task<IActionResult> Login(ClienteViewModel clienteViewModel)
        {
            var user = await _userManager.FindByNameAsync(clienteViewModel.Nome);
            if (user != null && await _userManager.CheckPasswordAsync(user, clienteViewModel.Senha))
            {
                var claim = new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserName) };
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
                int expiration = Convert.ToInt32(_configuration["Jwt:Expiration"]);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Desafio"],
                    audience: _configuration["Jwt:Desafio"],
                    expires: DateTime.UtcNow.AddMinutes(expiration),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                });
            }

            return Unauthorized();
        }
    }
}