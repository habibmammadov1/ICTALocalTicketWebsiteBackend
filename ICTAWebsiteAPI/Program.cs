using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Helpers;
using Business.Mapper;
using Core.Utilities.Filters;
using Data;
using Data.Abstract;
using Data.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(8080); // listen on all network interfaces (IPv4 + IPv6)
//});

// Add services to the container.

// DB Migration
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>();

// Mapper config
// Register IMapper in DI
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

Console.WriteLine(typeof(LdapConnection).FullName);

var conn = new LdapConnection();
foreach (var m in conn.GetType().GetMethods())
    Console.WriteLine(" - " + m.Name);

builder.Services.AddSingleton<IMapper>(config.CreateMapper());

builder.Services.AddHttpContextAccessor();

// Register service with DI container
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IAuthDal, EfAuthDal>();

builder.Services.AddScoped<IFooterService, FooterManager>();
builder.Services.AddScoped<IFooterDal, EfFooterDal>();

builder.Services.AddScoped<IRegulationsService, RegulationsManager>();
builder.Services.AddScoped<IRegulationsDal, EfRegulationsDal>();

builder.Services.AddScoped<ITeamMemberService, TeamMemberManager>();
builder.Services.AddScoped<ITeamMemberDal, EfTeamMembersDal>();

builder.Services.AddScoped<INoveltyService, NoveltyManager>();
builder.Services.AddScoped<IBaseNoveltyDal, EfBaseNoveltyDal>();

builder.Services.AddScoped<INoveltyFilesService, NoveltyFileManager>();
builder.Services.AddScoped<INoveltyFilesDal, EfNoveltyFilesDal>();

builder.Services.AddScoped<INoveltyLikeDal, EfNoveltyLikeDal>();

builder.Services.AddScoped<IBaseRulesService, BaseRulesManager>();
builder.Services.AddScoped<IBaseRulesDal, EfBaseRulesDal>();

builder.Services.AddScoped<IBaseRulesFilesPhotosService, BaseRulesFilesPhotosManager>();
builder.Services.AddScoped<IBaseRulesFilesPhotosDal, EfBaseRulesFilesPhotosDal>();

builder.Services.AddScoped<IFaqService, FaqManager>();
builder.Services.AddScoped<IFaqDal, EfFaqDal>();

builder.Services.AddScoped<IMeetingService, MeetingManager>();
builder.Services.AddScoped<IMeetingDal, EfMeetingDal>();

builder.Services.AddScoped<ILdapConnection1, LdapConnection1>();

builder.Services.AddScoped<ICompOfferService, CompOfferManager>();
builder.Services.AddScoped<ICompOfferDal, EfCompOfferDal>();

builder.Services.AddScoped<IGraphService, GraphManager>();

builder.Services.AddScoped<SetAuthorFilter>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// Add authorization services
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // optional
        ValidateAudience = false, // optional
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ICTA Local Website API",
        Version = "v1"
    });

    // 🔐 Add JWT Authorization to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Automate db migration
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate(); // Applies migrations or creates database
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ICTAWebsite API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
