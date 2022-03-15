using AutoMapper;
using Clean.Model.Output;
using Clean.TokenAuthentication;
using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly ITokenManager tokenManager;
        private readonly IAccountService<Account> service;
        private readonly ICompanyService<Company> CompanyService;
        private readonly IHotelService<Hotel> HotelService;

        private readonly IMapper mappper;

        public AuthenticateController(ITokenManager tokenManager, IAccountService<Account> service, ICompanyService<Company> CompanyService, IHotelService<Hotel> HotelService, IMapper mappper)
        {
            this.tokenManager = tokenManager;
            this.service = service;
            this.CompanyService = CompanyService;
            this.HotelService = HotelService;
            this.mappper = mappper;
        }

        [HttpGet]
        public IActionResult Authenticate(string email)
        {
            if (tokenManager.Authenticate(email).Result)
            {
                return Ok(new { Token = tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not unauthorized.");
                return Unauthorized(ModelState);
            }
        }

        [HttpGet]
        [Route("Email")]
        public async Task<ActionResult<EmailAccountOutputModel>> GetEmail()
        {
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            string email = tokenManager.getEmailFromToken(headerValue);
            if (email != null)
            {
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
            return Unauthorized();
        }
    }
}
