using BookStoreAPI.Helpers;
using BookStoreAPI.Model;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBookRepository _repo;
        public ProductsController(IBookRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles =AppRole.Customer)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _repo.getAllBooksAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = AppRole.Admin)]

        public async Task<IActionResult> GetById(int id)
        {
            var book=await _repo.GetBookByid(id);
            return book== null ? NotFound() : Ok(book);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            try
            {
                var NewBookId = await _repo.AddBookAsync(model);
                var book = await _repo.GetBookByid(NewBookId);
                return book == null ? NotFound() : Ok(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id,BookModel model)
        {
            await _repo.UpdateBook(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _repo.DeleteBook(id);
            return Ok();
        }

    }

  
    
}
