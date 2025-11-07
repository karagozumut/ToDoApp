using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoApp : ControllerBase
    {
        private static List<ToDoItem> _todos = new();


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_todos);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {


            var item = _todos.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }


        [HttpPost("Add")]
        public IActionResult Add(ToDoItem newItem)
        {
            newItem.Id = _todos.Count + 1;
            _todos.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, ToDoItem updatedItem)
        {
            var item = _todos.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            item.Title = updatedItem.Title;
            item.IsDone = updatedItem.IsDone;
            return NoContent();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var item = _todos.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            _todos.Remove(item);
            return NoContent();
        }
    }
}
