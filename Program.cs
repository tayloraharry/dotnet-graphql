using Api.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace dotnetcore_graphql
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args)
                .Build();

            using(var db = new StoreContext()) 
            {
                var authorDbEntry = db.Authors.Add(
                    new Author
                    {
                        Name = "Stephen King",
                    }
                );

                db.SaveChanges();

                db.Books.AddRange(
                new Book
                {
                    Name = "IT",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Mystery"
                },
                new Book
                {
                    Name = "The Langoleers",
                    Published = true,
                    AuthorId = authorDbEntry.Entity.Id,
                    Genre = "Mystery"
                }
                );

                db.SaveChanges();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}