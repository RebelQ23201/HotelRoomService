using AutoMapper;
using Clean.Model;
using Clean.Util;
using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text.Json;

namespace Clean.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountService<Account> accountService;
        //private readonly ICompanyService companyService;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Company> companyService, IMapper mappper)
        public AccountsController(IAccountService<Account> accountService, IMapper mappper)
        {
            this.accountService = accountService;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAccounts(
            [FromQuery] int? id, int? roleId, string email)
        {
            Expression<Func<Account, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.AccountId == id);
            }
            if (roleId != null)
            {
                filters = filters.AndAlso(c => c.RoleId == roleId);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                filters.AndAlso(c => c.Email == email);
            }
            List<Account> accounts = (await accountService.GetList(filters)).ToList();
            List<AccountModel> models = mappper.Map<List<AccountModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> GetAccount(int id)
        {
            Account account = await accountService.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            AccountModel model = mappper.Map<AccountModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            //_context.Entry(company).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CompanyExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            if (!await accountService.Update(account))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountModel>> PostAccount(Account account)
        {
            //_context.Companies.Add(company);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            if (!await accountService.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetAccounts), new { id = account.AccountId }, account);
        }
        
        [Route("SignInWithGoogle")]
        [HttpGet()]
        public async Task<ActionResult<AccountModel>> GetEmail(
            [FromQuery] string tokenid)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decodedValue = handler.ReadJwtToken(tokenid);
            
            JwtPayload payload = decodedValue.Payload;
            var json = payload.SerializeToJson();

            Dictionary<string?, object> sData = JsonSerializer.Deserialize<Dictionary<string?, object>>(json);
            string email = sData["email"].ToString();
            
            Account account = await accountService.GetEmail(email);

            if (account == null)
            {
                await accountService.CreateViaSignIn(email);
                Account accountAfterCreate = await accountService.GetEmail(email);
                AccountModel model2 = mappper.Map<AccountModel>(accountAfterCreate);
                return model2;
            }

            AccountModel model = mappper.Map<AccountModel>(account);

            return model;
        }
        

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            //var company = await _context.Companies.FindAsync(id);
            //if (company == null)
            //{
            //    return NotFound();
            //}

            //_context.Companies.Remove(company);
            //await _context.SaveChangesAsync();
            if (!await accountService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool CompanyExists(int id)
        //{
        //    return _context.Companies.Any(e => e.CompanyId == id);
        //}
    }
}
