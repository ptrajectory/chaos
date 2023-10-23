using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using chaos.Models;
using chaos.Services;
using chaos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Cryptography;
using chaos.utils;
using Microsoft.IdentityModel.Tokens;
using chaos.AuthRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using System.Reflection;
using chaos.Swagger;
using chaos.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http.Connections;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(); // load in all env variables before commencing build
var supabase_url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var supabase_api_key = Environment.GetEnvironmentVariable("SUPABASE_API_KEY");
var supabaseOptions = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};


// Add services to the container.

builder.Services.AddCors((options)=>{

    options.AddPolicy("AllowAllOrigins", (builder)=>{
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(o => {
    o.Events = new JwtBearerEvents(){
        OnMessageReceived = (context) => 
        {
            var path = context.HttpContext.Request.Path;

            if(path.StartsWithSegments("/chat-socket"))
            {
                var accessToken = context.Request.Query["access_token"];
                var bToken = context.Request.Headers.Authorization;


                Console.WriteLine($"Access Token {accessToken} b:: {bToken}");

                
                if(context.Request.Query.ContainsKey("access_token")){
                    context.Token = accessToken;
                }
            }

            return Task.CompletedTask;
        }
    };
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = false,
        RequireExpirationTime = false
    };
    var rsaKey = RSA.Create();

    rsaKey.ImportRSAPrivateKey(AppEnvironment.APP_RSA_KEY_PAIR, out _);

    o.Configuration = new OpenIdConnectConfiguration()
    {
        SigningKeys = {
            new RsaSecurityKey(rsaKey)
        }
    };

    o.MapInboundClaims = false;
});

builder.Services.AddAuthorization(options=>{
    options.AddPolicy(IdentityData.FriendPolicyName, p => p.RequireClaim(IdentityData.FriendClaimName, "true"));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ChatContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IChannel, chaos.Services.Channel>();
builder.Services.AddScoped<IUser, chaos.Services.User>();
builder.Services.AddScoped<IOrganization, chaos.Services.Organization>();
builder.Services.AddScoped<IApps, chaos.Services.App>();
builder.Services.AddScoped<IAuthService, AuthServive>();
builder.Services.AddScoped<AppResourcesAccessFilter>();
builder.Services.AddSingleton<IMessageSink, MessageService>();
builder.Services.AddHostedService(sp => {
    var sink = sp.GetService<IMessageSink>();
    return (MessageService)sink!; // TODO: figure out how to fix this
});
builder.Services.AddSingleton<IUserIdProvider, ChatHubIDProvider>();


builder.Services.AddSignalR(o => {
    o.AddFilter<ChatHubFilter>();
});
if(supabase_url != null  && supabase_api_key != null){
    builder.Services.AddSingleton<Supabase.Client>(new Supabase.Client(supabase_url, supabase_api_key, supabaseOptions));
}
builder.Services.AddScoped<IUpload, SupabaseUpload>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Other configurations...
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.OperationFilter<AddCustomHeaderParameter>();
    
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/chat-socket", o => {
    o.Transports = HttpTransportType.WebSockets;
});

app.Run();
