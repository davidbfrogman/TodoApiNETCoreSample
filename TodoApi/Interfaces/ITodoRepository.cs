using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodoRepository
    {
        Task<IActionResult> DoesTodoAlreadyExist(int id);
        Task<IActionResult> All();
        Task<IActionResult> Find(int id);
        Task<IActionResult> Insert(Todo item);
        Task<IActionResult> Update(int id, Todo item);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> SearchForUncompletedTodos();
        Task<IActionResult> SearchForCompeletedTodos();
    }
}
