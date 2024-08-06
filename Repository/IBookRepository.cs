using BookStoreAPI.Data;
using BookStoreAPI.Model;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> getAllBooksAsync();
        public Task<BookModel> GetBookByid(int id);
        public Task<int> AddBookAsync(BookModel model);
        public Task UpdateBook(int id, BookModel model);
        public Task DeleteBook(int id);



    }
}
