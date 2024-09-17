using fabarblog.Repository;
using fabarblog.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Registrar os serviços necessários, incluindo os controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o DbContext com a conexão ao PostgreSQL
builder.Services.AddDbContext<Context>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Registrar serviços do repositório e outros serviços de dependência
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostService>();

var app = builder.Build();

// Configurações para ambiente de desenvolvimento, como o Swagger
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();

// Middleware de autorização (se necessário)
app.UseAuthorization();

// Mapeamento dos controladores para as rotas
app.MapControllers();

app.Run();
