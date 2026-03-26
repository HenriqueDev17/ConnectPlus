using ConnectPlus.WebAPI.BdContextConnect;
using ConnectPlus.WebAPI.Controllers;
using ConnectPlus.WebAPI.Interfaces;
using ConnectPlus.WebAPI.Models;
using ConnectPlus.WebAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ConnectContext>(options => options.UseSqlServer //inserir a string de conexão do banco de dados aqui, ou usar o appsettings.json para armazenar a string de conexão e ler usando builder.Configuration.GetConnectionString("DefaultConnection")
(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Registrar os reposirories para injeção de dependência
builder.Services.AddScoped<ITipoDeContatoRepository, TipoDeContatoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();



//Adicione o serviço de autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";

})

.AddJwtBearer("JwtBearer", Options =>
{
    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true, //Valida quem está solicitando o token
        ValidateAudience = true, //valida quem está recebendo o token
        ValidateLifetime = true,//define se o token tem um tempo de expiração

        //Chave de acesso ao token
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ConnectPlus-chave-autenticacao-webapi-dev")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(7),

        //nome do issuer (de onde o token está vindo)
        ValidIssuer = "api_ConnectPlus",

        //nome da audience (para onde ele está indo)
        ValidAudience = "api_ConnectPlus"
    };
});


//Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de ConnectPlus",
        Description = "Aplicação para gerenciamento de ConnectPlus",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Matheus Felix",
            Url = new Uri("https://www.linkedin.com/in/matheus")
        },
        License = new OpenApiLicense
        {
            Name = "Exemplo de licensa",
            Url = new Uri("https://opensource.org/licenses")
        }

    });

    //Usando a autenticação no swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT: "
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
