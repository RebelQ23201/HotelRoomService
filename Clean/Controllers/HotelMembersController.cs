using AutoMapper;
using Clean.Model.Output;
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
    [Route("api/HotelMember")]
    [ApiController]
    public class HotelMembersController : Controller
    {
        private readonly IBaseService<HotelMember> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<HotelMember> accountService, IMapper mappper)
        public HotelMembersController(IBaseService<HotelMember> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelMemberOutputModel>>> GetHotelMembers(
            [FromQuery] int? id)
        {
            Expression<Func<HotelMember, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.MemberId == id);
            }

            List<HotelMember> accounts = (await service.GetList(filters)).ToList();
            List<HotelMemberOutputModel> models = mappper.Map<List<HotelMemberOutputModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelMemberOutputModel>> GetHotelMember(int id)
        {
            HotelMember account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            HotelMemberOutputModel model = mappper.Map<HotelMemberOutputModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelMember(int id, HotelMember account)
        {
            if (id != account.MemberId)
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
        public async Task<ActionResult<HotelMemberOutputModel>> PostHotelMember(HotelMember account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetHotelMembers), new { id = account.MemberId }, account);
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
