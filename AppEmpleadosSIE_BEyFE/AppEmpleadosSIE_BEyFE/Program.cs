using AppEmpleadosSIE_BEyFE.Data.Interfaces;
using AppEmpleadosSIE_BEyFE.Data.Repositories;
using AppEmpleadosSIE_BEyFE.Services;
using JobOclock_BackEnd.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides; // ← AGREGAR ESTA LÍNEA
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔧 CONFIGURACIÓN PARA RENDER - Puerto dinámico
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 1️⃣ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 2️⃣ Inyección de dependencias para repositorios
var connStr = builder.Configuration.GetConnectionString("EmpleadosSIE");
builder.Services.AddScoped<IUsuarioRepository>(_ => new UsuarioRepository(connStr));
builder.Services.AddScoped<IUsuarioXServicioRepository>(_ => new UsuarioXServicioRepository(connStr));

// 3️⃣ Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// 4️⃣ Servicio unificado
builder.Services.AddScoped<IServicesSIE, ServicesSIE>();

// 5️⃣ MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🚀 Habilitar explorar directorios (opcional)
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// 🔧 CONFIGURACIÓN PARA RENDER - Headers de proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// 6️⃣ Pipeline - Swagger también en producción para testing
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ Comentar HTTPS redirect para Render
// app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// 🌐 Servir archivos estáticos desde wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// 👉 Fallback: si no encuentra ruta, devuelve el index.html de Pages
app.MapFallbackToFile("Pages/Login_page.html");

// Mapear controladores (API)
app.MapControllers();

app.Run();