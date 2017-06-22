using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepositoryPrivateList : ITodoRepository
    {
        private readonly List<Todo> _todos = new List<Todo>();

        public async Task<IActionResult> All()
        {
            if(_todos.Count() == 0)
            {
                _todos.Add(new Todo() { Id = 1, IsComplete = false, Name = "This is a list todo" });
            }
            return new ObjectResult(_todos);
        }

        public async Task<IActionResult> Delete(int id)
        {
            _todos.Remove(_todos.Find(td =>td.Id == id));
            return new ObjectResult(id);
        }

        public async Task<IActionResult> DoesTodoAlreadyExist(int id)
        {
            return  new ObjectResult(_todos.Any(td => td.Id == id));
        }

        public async Task<IActionResult> Find(int id)
        {
            return new ObjectResult(_todos.Find(td => td.Id == id));
        }

        public async Task<IActionResult> Insert(Todo item)
        {
            _todos.Add(item);
            return new ObjectResult(item);
        }

        public async Task<IActionResult> SearchForCompeletedTodos()
        {
            return new ObjectResult(_todos.Where(td => td.IsComplete == true).ToList());
        }

        public async Task<IActionResult> SearchForUncompletedTodos()
        {
            return new ObjectResult(_todos.Where(td => td.IsComplete == false).ToList());
        }

        public async Task<IActionResult> Update(int id, Todo item)
        {
            var itemToUpdate = _todos.Find(td => td.Id == id);
            var index = _todos.IndexOf(itemToUpdate);
            _todos.RemoveAt(index);
            _todos.Insert(index, item);
            return new ObjectResult(item);
        }
    }
}
