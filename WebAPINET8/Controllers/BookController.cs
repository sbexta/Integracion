using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINET8.Database;
using WebAPINET8.Models;

namespace WebAPINET8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound($"No se encontró un libro con ID {id}.");
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Books.FindAsync(id);
            if (existing == null)
                return NotFound($"No se encontró un libro con ID {id}.");

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Price = book.Price;
            existing.Stock = book.Stock;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound($"No se encontró un libro con ID {id}.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
