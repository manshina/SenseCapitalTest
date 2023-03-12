using AutoMapper;
using BCrypt.Net;
using jwtauth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SenseCapitalTest.Data;
using SenseCapitalTest.Dtos.Account;

using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace SenseCapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //контроллр для авторизации и регистрации
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IMapper _mapper;
        public AuthController(DataContext context, IOptions<AuthOptions> authOptions, IMapper mapper)
        {
            _context = context;
            _authOptions = authOptions;
            _mapper = mapper;
        }
        //регистрация нового пользователя
        [HttpPost("Registr")]
        public async Task<IActionResult> Registr(RegistrDto registrDto)
        {
            
            Account account = _mapper.Map<Account>(registrDto);
            //в бд храним хэш пароли
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password);
            if(_context.accounts.FirstOrDefault(a=>a.UserName == account.UserName) !=null)
            {
                return BadRequest("username is already taken");
            }
            account.Password = passwordHash;
            await _context.accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //логин выдает JWT токен, который необходимо указать в хедере
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {


            Account account = _mapper.Map<Account>(loginDto);

            
            var user = _context.accounts.FirstOrDefault(a=>a.UserName == account.UserName);
            if(user == null)
            {
                return Unauthorized();
            }
            if(!BCrypt.Net.BCrypt.Verify(account.Password, user.Password))
            {
                return Unauthorized();
            }
            //var user = Authorize(request.UserName, request.Password);
            var token = CreateJWT(user);
            return Ok(new
            {
                token
            });
        }

        //функция для создания JWT токена
        private string CreateJWT(Account account)
        {
            var authParams = _authOptions.Value;
            var secKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub,account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.UserName),                
                new Claim("role", account.Roles.ToString())
            };


            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
