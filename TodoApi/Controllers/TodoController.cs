using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        /*[HttpGet]
        [Route("teste")]
         public List<Todo> Get()
        {
            return new List<Todo>();
        } */

        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetTodosAsync([FromServices] AppContexto context)
        {
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetAsyncid(
            [FromServices] AppContexto context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return todo == null
                ? NotFound()
                : Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppContexto context,
            [FromForm] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Title = model.Title,
                Done = false,
                Date = DateTime.Now
       
            };

            try
            {
                //salva na memoria
                await context.Todos.AddAsync(todo);

                //salva no bando de dados
                await context.SaveChangesAsync();

                return Created($"v1/todos/{todo.Id}", todo);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppContexto context,
            [FromBody] CreateTodoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.Title = model.Title;

                context.Todos.Update(todo);

                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppContexto contexto,
            [FromRoute] int id)
        {
            var todo = await contexto.Todos.FirstOrDefaultAsync(x => x.Id == id);

            contexto.Todos.Remove(todo);
            await contexto.SaveChangesAsync();
            return Ok();

        }

    }
}
