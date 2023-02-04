using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("HCAPI", new OpenApiInfo()
    {
        Title = "RESTful API",
        Version = "V1",
        Description = "Restful Api Trials",
        Contact = new OpenApiContact()
        {
            Email = "enes.serenli@hotmail.com",
            Name = "Enes Serenli",
            Url = new Uri("https://github.com/EnesSERENLI/AnadoluPrmPracticum")
        },
        License = new OpenApiLicense()
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/HCAPI/swagger.json", "HC API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
