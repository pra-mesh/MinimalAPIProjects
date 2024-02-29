using BasicMinimalAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapGet("/Student", async (DataContext context) =>
{
  return await context.Students.ToListAsync();

});
app.MapGet("/Student/{id}", async (DataContext context, int id) =>
{

  return await context.Students.FindAsync(id) is Student student ?
    Results.Ok(student) :
      Results.NotFound("Invalid Student Id");
});

app.MapPost("/Student", async (DataContext context, Student student) =>
{
  context.Students.Add(student);
  await context.SaveChangesAsync();
  return Results.Ok(await context.Students.ToListAsync());
});


app.MapPut("/Student/{id}", async (DataContext context, int id, Student updateStudent) =>
{
  var student = await context.Students.FindAsync(id);
  if (student is null)
    return Results.NotFound("Invalid Student Id");
  context.Students.Update(student);
  await context.SaveChangesAsync();
  return Results.Ok(updateStudent);
});

app.MapDelete("/Student/{id}", async (DataContext context, int id) =>
{
  var student = await context.Students.FindAsync(id);
  if (student is null)
    return Results.NotFound("Invalid Student Id");
  context.Students.Remove(student);
  context.SaveChanges();
  return Results.Ok("Deleted");

});
app.Run();

