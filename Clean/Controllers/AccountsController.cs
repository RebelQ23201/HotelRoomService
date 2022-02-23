using AutoMapper;
using Clean.Model;
using Clean.Util;
using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clean.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IBaseService<Account> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<Account> accountService, IMapper mappper)
        public AccountsController(IBaseService<Account> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAccounts(
            [FromQuery] int? id)
        {
            Expression<Func<Account, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.AccountId == id);
            }

            List<Account> accounts = (await service.GetList(filters)).ToList();
            List<AccountModel> models = mappper.Map<List<AccountModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> GetAccount(int id)
        {
            Account account = await service.GetById(id);

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

            if (!await service.Update(account))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountModel>> PostAccount(Account account)
        { if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetAccounts), new { id = account.AccountId }, account);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
