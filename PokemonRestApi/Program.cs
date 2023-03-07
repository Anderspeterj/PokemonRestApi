using PokemonRestApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<PokemonsRepositories>(new PokemonsRepositories());
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();






// Configure the HTTP request pipeline.
app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();




app.Run();
