using DemoApplication1.DbContexts;
using DemoApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<PersonService>();
builder.Services.AddTransient<ShoppingItemService>();
builder.Services.AddTransient<PersonContext>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddTransient<ShoppingItemContext>();
builder.Services.AddScoped<ShoppingItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
