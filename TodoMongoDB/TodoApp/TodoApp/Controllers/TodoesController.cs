using TodoApp.Models;
using TodoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class TodoesController : ControllerBase
    {
    private readonly TodoesService _todoesService;

    public TodoesController(TodoesService todoesService) =>
        _todoesService = todoesService;

    [HttpGet]
    public async Task<List<Todo>> Get() =>
        await _todoesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Todo>> Get(string id)
    {
        var todoItem = await _todoesService.GetAsync(id);

        if (todoItem is null)
        {
            return NotFound();
        }

        return todoItem;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Todo newTodoItem)
    {
        await _todoesService.CreateAsync(newTodoItem);

        return CreatedAtAction(nameof(Get), new { id = newTodoItem.Id }, newTodoItem);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Todo updatedTodoItem)
    {
        var todoItem = await _todoesService.GetAsync(id);

        if (todoItem is null)
        {
            return NotFound();
        }

        updatedTodoItem.Id = todoItem.Id;

        await _todoesService.UpdateAsync(id, updatedTodoItem);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var todoItem = await _todoesService.GetAsync(id);

        if (todoItem is null)
        {
            return NotFound();
        }

        await _todoesService.RemoveAsync(id);

        return NoContent();
    }
}




