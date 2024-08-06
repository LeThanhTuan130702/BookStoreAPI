using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookStoreContext:IdentityDbContext<ApplicationUsers>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> opt): base(opt)
        {

        }
        #region Dbset
      public  DbSet<Book>? Books { get; set; }
        #endregion

    }
}
