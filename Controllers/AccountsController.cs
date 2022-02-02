using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Data;
using Catalog.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Catalog.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace BSB_test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<AccountAuthDTO>> Login(LoginAccountDTO model)
        {
            Account user = await _accountRepo.GetAsync(model.Login, model.Password);

            if (user != null)
            {
                await AuthenticateAsync(user);
                var accountDTO = _mapper.Map<AccountGetDTO>(user);
                return Ok(accountDTO);
            }
            else
            {
                return BadRequest("Wrong login or password");
            }
        }

        private async Task AuthenticateAsync(Account user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "UserCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }

        // GET: api/Accounts
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AccountGetDTO>> GetAccounts()
        {
            var acc = await _accountRepo.GetByNameAsync(User.Identity.Name);

            return _mapper.Map<AccountGetDTO>(acc);
        }

        //// GET: api/Accounts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Account>> GetAccount(int id)
        //{
        //    var account = await _context.Accounts.FindAsync(id);

        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return account;
        //}

        //// PUT: api/Accounts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccount(int id, Account account)
        //{
        //    if (id != account.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(account).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccountExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(int id)
        //{
        //    var account = await _context.Accounts.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Accounts.Remove(account);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AccountExists(int id)
        //{
        //    return _context.Accounts.Any(e => e.Id == id);
        //}
    }
}
