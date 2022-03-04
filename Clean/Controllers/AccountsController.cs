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
        private readonly IAccountService<Account> service;
        private readonly IMapper mappper;

        public AccountsController(IAccountService<Account> service, IMapper mappper)
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
        {
            if (!await service.Create(account))
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
            //get JWT token
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decodedValue = handler.ReadJwtToken(tokenid);
            
            //get payload, data from the token and turn into json
            JwtPayload payload = decodedValue.Payload;
            var json = payload.SerializeToJson();

            //get each data from the json and get user email
            Dictionary<string?, object> sData = JsonSerializer.Deserialize<Dictionary<string?, object>>(json);
            string email = sData["email"].ToString();
            
            //search email from database
            Account account = await service.GetEmail(email);

            //if not exist create one and log in as that user
            if (account == null)
            {
                await service.CreateViaSignIn(email);
                Account accountAfterCreate = await service.GetEmail(email);
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
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
