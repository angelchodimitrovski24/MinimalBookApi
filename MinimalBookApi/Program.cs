var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book> { 
   new Book {Id = 1, Title = "Book Title 1", Author = "Author 1"},
   new Book {Id = 2, Title = "Book Title 2", Author = "Author 2"},
   new Book {Id = 3, Title = "Book Title 3", Author = "Author 3"}
};

app.MapGet("/book", () =>
{
    return books;
});

app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("This book is not in the database");

    return Results.Ok();
});

app.MapPost("/books", (Book boook) =>
{
    books.Add(boook);
    return books;
});

app.MapPut("/book/{id}", (Book updateBook, int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("This book is not in the database");
    
    book.Title = updateBook.Title;
    book.Author = updateBook.Author;

    return Results.Ok();
});

app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);

    if (book is null)
        return Results.NotFound("This book is not in the database");

    books.Remove(book);

    return Results.Ok();
});

app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}