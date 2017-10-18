using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.Services;
using AspNetCoreTodoPOC.Models;

namespace AspNetCoreTodoPOC.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(NewTodoItem newItem)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
            };

            _context.Items.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            return await _context.Items
                .Where(x => !x.IsDone)
                .ToArrayAsync();

        }
    }
}
