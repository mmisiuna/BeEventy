using BeEventy.Data.Models;
using BeEventy.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PostgreSQL.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static BeEventy.Data.Models.Login;

namespace BeEventy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("/name/{name}")]
        public async Task<ActionResult<Account>> GetAccountByName(string name)
        {
            var account = await _accountRepository.GetAccountByNameAsync(name);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("/email/{email}")]
        public async Task<ActionResult<Account>> GetAccountByEmail(string email)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(email);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(Account account)
        {
            await _accountRepository.AddAccountAsync(account);
            return CreatedAtAction(nameof(GetAllAccounts), new { id = account.Id }, account);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountById(int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(request.Login);

            if (account != null && account.Password == request.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superTajneHasłoooooooooooooooooooooooooooooooo"));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5100",
                    audience: "http://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                // Zwróć ID zalogowanego użytkownika razem z tokenem JWT
                return Ok(new LoginResponse(token, account.Id));
            }

            return Unauthorized();
        }

    }
}
