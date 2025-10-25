using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Data;

namespace MinimalApiDemo.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/v1/books");

            group.MapGet("/", GetBooks);
            group.MapGet("/{id}", GetBookById);
        }

        private static async Task<IResult> GetBooks(BookContext bookContext)
        {
            var books = await bookContext.Books.ToListAsync();
            return Results.Ok(books);

        }
        private static async Task<IResult> GetBookById(BookContext bookContext, int id)
        {
            var book = await bookContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(book);
            }
        }
    }
}
