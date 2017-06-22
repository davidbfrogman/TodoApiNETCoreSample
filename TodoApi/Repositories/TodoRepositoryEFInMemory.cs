using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Repositories
{
    public class TodoRepositoryEFInMemory : ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepositoryEFInMemory(TodoContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> All()
        {
            if(_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new Todo() { Id = 1, IsComplete = false, Name = "Go walk the dogs" });
                _context.SaveChanges();
            }
            return new ObjectResult(await _context.TodoItems.ToListAsync());
        }

        public async Task<IActionResult> DoesTodoAlreadyExist(int id)
        {
            if(id == 0)
            {
                return new BadRequestResult();
            }
            return new ObjectResult(await this._context.TodoItems.FirstOrDefaultAsync(ti => ti.Id == id) != default(Todo));
        }

        public async Task<IActionResult> Find(int id)
        {
            var todo = await this._context.TodoItems.FirstOrDefaultAsync(ti => ti.Id == id);
            if (todo == default(Todo))
            {
                return new NotFoundResult();
            }
            return new ObjectResult(todo);
        }

        public async Task<IActionResult> Insert(Todo item)
        {
            if (item == default(Todo))
            {
                return new BadRequestResult();
            }

            this._context.TodoItems.Add(item);
            await this._context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetTodo", new { id = item.Id }, item);
        }

        public async Task<IActionResult> SearchForCompeletedTodos()
        {
           return new ObjectResult(await _context.TodoItems.Where(ti => ti.IsComplete == true).ToListAsync());
        }

        public async Task<IActionResult> SearchForUncompletedTodos()
        {
            return new ObjectResult(await _context.TodoItems.Where(ti => ti.IsComplete != true).ToListAsync());
        }

        public async Task<IActionResult> Update(int id, Todo item)
        {
            if (item == default(Todo) || item.Id != id)
            {
                return new BadRequestResult();
            }
            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == default(Todo))
            {
                return new NotFoundResult();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.TodoItems.FirstAsync(t => t.Id == id);
            if (todo == default(Todo))
            {
                return new NotFoundResult();
            }
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

    }
}
