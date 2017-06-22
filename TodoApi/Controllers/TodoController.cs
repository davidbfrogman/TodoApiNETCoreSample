using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Interfaces;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await this._repository.All();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return await this._repository.Find(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Todo item)
        {
            return await this._repository.Insert(item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Todo item)
        {
            return await this._repository.Update(id, item);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this._repository.Delete(id);


            return new JsonResult(new { deletedId = id, message = "Successfully deleted item" });
        }
    }
}
