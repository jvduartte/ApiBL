using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiBiblioteca.ORM;
using WebApiBiblioteca.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicione o contexto do banco de dados
builder.Services.AddDbContext<BdBiblioetcadvjContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicione o repositório UsuarioR
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<CategoriaRepositorio>();
builder.Services.AddScoped<EmprestimoRepositorio>();
builder.Services.AddScoped<LivroRepositorio>();
builder.Services.AddScoped<MembroRepositorio>();
builder.Services.AddScoped<ReservaRepositorio>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoWebAPI", Version = "v1" });

    // Configura o Swagger para usar o Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato **Bearer {seu_token}**",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoWebAPI v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Habilitar autenticação
app.UseAuthorization();  // Habilitar autorização

app.MapControllers();

app.Run();
