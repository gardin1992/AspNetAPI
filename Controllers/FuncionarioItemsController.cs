using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioItemsController : ControllerBase
    {
        private readonly FuncionarioContext _context;

        public FuncionarioItemsController(FuncionarioContext context)
        {
            _context = context;
        }

        // GET: api/FuncionarioItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/FuncionarioItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioItem>> GetFuncionarioItem(int id)
        {
            var funcionarioItem = await _context.TodoItems.FindAsync(id);

            if (funcionarioItem == null)
            {
                return NotFound();
            }

            return funcionarioItem;
        }

        // PUT: api/FuncionarioItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionarioItem(int id, FuncionarioItem funcionarioItem)
        {
            if (id != funcionarioItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(funcionarioItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FuncionarioItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FuncionarioItem>> PostFuncionarioItem(FuncionarioItem funcionarioItem)
        {
            _context.TodoItems.Add(funcionarioItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetFuncionarioItem", new { id = funcionarioItem.Id }, funcionarioItem);
            return CreatedAtAction(nameof(GetFuncionarioItem), new { id = funcionarioItem.Id }, funcionarioItem);
        }

        // DELETE: api/FuncionarioItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FuncionarioItem>> DeleteFuncionarioItem(int id)
        {
            var funcionarioItem = await _context.TodoItems.FindAsync(id);
            if (funcionarioItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(funcionarioItem);
            await _context.SaveChangesAsync();

            return funcionarioItem;
        }

        private bool FuncionarioItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
