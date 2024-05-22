using Challenge2_DotNet.Functions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenGeneration, TokenGeneration>();
builder.Services.AddScoped<IRetrieveMetadata, RetrieveMetadata>();
builder.Services.AddHttpClient<ITokenGeneration, TokenGeneration>();
builder.Services.AddHttpClient<IRetrieveMetadata, RetrieveMetadata>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
