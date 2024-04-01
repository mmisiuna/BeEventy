using BeEventy.Data.Repositories;
using BeEventy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostgreSQL.Data;

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
    }
}
