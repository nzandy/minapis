using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer(); // Important for minimal APIs!
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<BookContext>();
    dbContext.Database.EnsureCreated();
}


app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/v1/books", GetBooks);

app.Run();



return;

async Task<IResult> GetBooks(BookContext bookContext)
{
    var books = await bookContext.Books.ToListAsync();
    return Results.Ok(books);

};