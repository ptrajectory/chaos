using Microsoft.EntityFrameworkCore;
using chaos.Models;
using chaos.Services;
using chaos;


var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(); // load in all env variables before commencing build
var supabase_url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var supabase_api_key = Environment.GetEnvironmentVariable("SUPABASE_API_KEY");
var supabaseOptions = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ChatContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IChannel, chaos.Services.Channel>();
builder.Services.AddScoped<IUser, chaos.Services.User>();
if(supabase_url != null  && supabase_api_key != null){
    builder.Services.AddSingleton<Supabase.Client>(new Supabase.Client(supabase_url, supabase_api_key, supabaseOptions));
}
builder.Services.AddScoped<IUpload, SupabaseUpload>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
