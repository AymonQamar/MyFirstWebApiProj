using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProj.Models;

namespace MyFirstWebApiProj.Data

{
    public class FirstApiContext : DbContext
    {
        public FirstApiContext(DbContextOptions<FirstApiContext> options) : base(options)
        { 
        
        }

        public DbSet<Book> books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "1984",
                    Author = "George Orwell",
                    YearPublished = 2004

                },
            new Book
            {
                Id = 2,
                Title = "Sapiens",
                Author = "Yuval Noah Harari",
                YearPublished = 2011
            },
            new Book
            {
                Id = 3,
                Title = "Recdonstruction of Religious Thoughts in Islam",
                Author = "Muhammad Iqbal",
                YearPublished = 1930
            }
            );
        }       
    }
}
