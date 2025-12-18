using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Librarymanagement_RAZOR_CFA_.Models
{
    public class LibraryDbContext : DbContext
    {


        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        public DbSet<BookModel> BookTable { get; set; }
    }
}
