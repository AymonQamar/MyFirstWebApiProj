using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProj.Data;
using MyFirstWebApiProj.Models;
using MyFirstWebApiProj.DTOs;



namespace MyFirstWebApiProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //static private List<Book> books = new List<Book>
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Title = "1984",
        //        Author = "George Orwell",
        //        YearPublished = 2004

        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Title = "Sapiens",
        //        Author = "Yuval Noah Harari",
        //        YearPublished = 2011
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Title = "Recdonstruction of Religious Thoughts in Islam",
        //        Author = "Muhammad Iqbal",
        //        YearPublished = 1930
        //    }

        //};
        //<----DI---->
        //Injecting Dependency
        private readonly FirstApiContext _context;       
        public BooksController(FirstApiContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task <ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.books.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByID(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }
        [HttpPost("Filter-even")]
        public async Task<IActionResult> GetEvenNumbers([FromBody] NumberRequest request)
        {
            
            if (request.Numbers == null || !request.Numbers.Any())
            {
                return BadRequest("List of numbers can not be empty");
            }
            var evens = request.Numbers.Where(n => n % 2  == 0);
            return Ok(evens);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();
           _context.books.Add(newBook);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookByID), new { id = newBook.Id }, newBook);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.books.FindAsync(id);

            if (book == null)
                return NotFound();

            // Update fields (but NOT Id!)
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            var book = _context.books.FindAsync(id);
            if (book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
