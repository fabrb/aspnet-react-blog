using System.Text;
using fabarblog.Data;
using fabarblog.Repository;
using fabarblog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
			builder => builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
});

builder.Services.AddDbContext<Context>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings["Issuer"],
			ValidAudience = jwtSettings["Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
		};
	});

builder.Services.AddAuthorizationBuilder()
	.AddPolicy("superuser",
		policy => policy
			.RequireRole("admin")
			.RequireClaim("scope", "list.users")
	);

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<UserRepository>();

// Post Services
builder.Services.AddScoped<CreatePost>();
builder.Services.AddScoped<EditPost>();
builder.Services.AddScoped<DeletePost>();
builder.Services.AddScoped<ListPosts>();
builder.Services.AddScoped<SearchPost>();

// User Services
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<EditUser>();
builder.Services.AddScoped<DeleteUser>();
builder.Services.AddScoped<ListUsers>();
builder.Services.AddScoped<SearchUser>();

// Authentication Services
builder.Services.AddScoped<AuthenticateUser>();
builder.Services.AddScoped<VerifyAuthentication>();
builder.Services.AddScoped<GenerateAuthenticationToken>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
