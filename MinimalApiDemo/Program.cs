var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer(); // Important for minimal APIs!
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
app.MapGet("/v1/books", GetBooks);

app.Run();

static async Task<IResult> GetBooks()
{
    var bookList = new List<Book>()
    {
        new Book
        {
            Id = 1,
            Title = "The great gatsby"
        },
        new Book
        {
            Id = 2,
            Title = "War and peace"
        }
    };
    return await Task.FromResult(Results.Ok(bookList));
};