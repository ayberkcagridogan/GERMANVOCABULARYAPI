using System.Text;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<GermanVocabularyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var key = Encoding.UTF8.GetBytes("jPkboYFItP6ThjpCWnmu52QgMBIyZeDqnAwRvPz2zB8");


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GermanVocabularyAPI", Version = "v1" });
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "German Vocabulary")); 
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();