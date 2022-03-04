﻿using AutoMapper;
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
    [Route("api/RoomService")]
    [ApiController]
    public class RoomServicesController : Controller
    {
        private readonly IBaseService<RoomService> service;
        private readonly IMapper mappper;

        //public CompaniesController(IBaseService<RoomService> accountService, IMapper mappper)
        public RoomServicesController(IBaseService<RoomService> service, IMapper mappper)
        {
            this.service = service;
            this.mappper = mappper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomServiceModel>>> GetRoomServices(
            [FromQuery] int? id)
        {
            Expression<Func<RoomService, bool>> filters = c => true;
            if (id != null)
            {
                filters = filters.AndAlso(c => c.RoomServiceId == id);
            }

            List<RoomService> accounts = (await service.GetList(filters)).ToList();
            List<RoomServiceModel> models = mappper.Map<List<RoomServiceModel>>(accounts);
            return models;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomServiceModel>> GetRoomService(int id)
        {
            RoomService account = await service.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            RoomServiceModel model = mappper.Map<RoomServiceModel>(account);

            return model;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomService(int id, RoomService account)
        {
            if (id != account.RoomServiceId)
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
        public async Task<ActionResult<RoomServiceModel>> PostRoomService(RoomService account)
        {
            if (!await service.Create(account))
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetRoomServices), new { id = account.RoomServiceId }, account);
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