using ProductAuthApi.Persistance;
using ProductAuthApi.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistenceServices(
	builder.Configuration.GetConnectionString("PostgreSqlConnection")
);
builder.Services.AddInfrastructureServices(builder.Configuration);

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
});
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration["Redis:Configuration"];
	options.InstanceName = builder.Configuration["Redis:InstanceName"];
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
	var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);

	c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductAuthApi", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header kullanimi. Ornek: 'Bearer {token}'"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();

