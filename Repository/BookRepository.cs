using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context,IMapper mapper) {
            _context = context;
            _mapper=mapper;
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            var newbook=_mapper.Map<Book>(model);
            _context.Books!.Add(newbook);
            await _context.SaveChangesAsync();
            return newbook.Id;
        }

        public async Task DeleteBook(int id)
        {
            var DeleteBook=_context.Books!.SingleOrDefault(x => x.Id == id);
            if(DeleteBook!=null)
            {
                _context.Books!.Remove(DeleteBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookModel>> getAllBooksAsync()
        {
            var books = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookByid(int id)
        {
            var book = await _context.Books!.FindAsync(id);
            return _mapper.Map<BookModel>(book);

        }

        public async Task UpdateBook(int id, BookModel model)
        {
            if(id==model.Id)
            {
                var updatebook=_mapper.Map<Book>(model);
                _context.Books!.Update(updatebook);
                await _context.SaveChangesAsync();
            }    
        }

       
    }
}
