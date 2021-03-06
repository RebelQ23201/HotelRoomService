using AutoMapper;
using Clean.Model.Output;
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
using Clean.Filter;
using Clean.Model.Input;

namespace Clean.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountService<Account> service;
        private readonly ICompanyService<Company> CompanyService;
        private readonly IHotelService<Hotel> HotelService;

        private readonly IMapper mappper;

        
        public AccountsController(IAccountService<Account> service, ICompanyService<Company> CompanyService, IHotelService<Hotel> HotelService, IMapper mappper)
        {
            this.service = service;
            this.CompanyService = CompanyService;
            this.HotelService = HotelService;
            this.mappper = mappper;
        }

        [HttpGet]
        [TokenAuthenticationFilter]
        public async Task<ActionResult<IEnumerable<AccountOutputModel>>> GetAccounts(
            [FromQuery] int? id, bool? detailed=false)
        {
            Expression<Func<Account, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.AccountId == id);
            }

            List<Account> accounts = (await service.GetList(filters, detailed)).ToList();
            List<AccountOutputModel> models = mappper.Map<List<AccountOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        [TokenAuthenticationFilter]
        public async Task<ActionResult<AccountOutputModel>> GetAccount(int id, bool? detailed = true)
        {
            Account account = await service.GetById(id, detailed);

            if (account == null)
            {
                return NotFound();
            }

            AccountOutputModel model = mappper.Map<AccountOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [TokenAuthenticationFilter]
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
        [TokenAuthenticationFilter]
        public async Task<ActionResult<AccountOutputModel>> PostAccount(Account account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetAccounts), new { id = account.AccountId }, account);
        }
        
        [Route("SignInWithGoogle")]
        [HttpGet()]
        public async Task<ActionResult<EmailAccountOutputModel>> GetEmail(
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
                return Unauthorized();
            }

            EmailAccountOutputModel model = new EmailAccountOutputModel();
            model.AccountId = account.AccountId;
            model.Email = account.Email;
            model.RoleId = account.RoleId;
            Company company = await CompanyService.GetEmail(email);
            Hotel hotel = await HotelService.GetEmail(email);
            if (company == null)
            {
                model.CompanyId = null;
            }
            else
            {
                model.CompanyId = company.CompanyId;
            }
            if (hotel == null)
            {
                model.HotelId = null;
            }
            else
            {
                model.HotelId = hotel.HotelId;
            }
            return model;
        }
        

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<AccountOutputModel>> Register(RegisterInputModel registerInput)
        {
            Account checkAccount = await service.GetEmail(registerInput.Email);
            if (checkAccount != null)
            {
                return BadRequest();
            }

            Account account = new Account();
            account.Email = registerInput.Email;
            account.RoleId = registerInput.RoleId;
            if (!await service.Create(account))
            {
                return NotFound();
            }

            if (registerInput.RoleId == 2)
            {
                int companyId = await CompanyService.GetTotal();
                Company company = new Company();
                company.CompanyId = companyId + 1;
                company.Email = registerInput.Email;
                company.Address = registerInput.Address;
                company.Name = registerInput.Name;
                company.Phone = registerInput.Phone;
                if (!await CompanyService.Create(company))
                {
                    return NotFound();
                }
            }
            if (registerInput.RoleId == 3)
            {
                int hotelId = await HotelService.GetTotal();
                Hotel hotel = new Hotel();
                hotel.HotelId = hotelId + 1;
                hotel.Email = registerInput.Email;
                hotel.Address = registerInput.Address;
                hotel.Name = registerInput.Name;
                hotel.Phone = registerInput.Phone;
                if (!await HotelService.Create(hotel))
                {
                    return NotFound();
                }
            }
            return CreatedAtAction(nameof(GetAccounts), new { id = account.AccountId }, account);
        }
    }
}
