﻿using AppEmpleadosSIE_BEyFE.Data.Interfaces;
using AppEmpleadosSIE_BEyFE.Data.Repositories;
using AppEmpleadosSIE_BEyFE.Services;
using JobOclock_BackEnd.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

// 6️⃣ Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// 🌐 Servir archivos estáticos desde wwwroot
app.UseDefaultFiles();   // sirve automáticamente index.html si está en wwwroot
app.UseStaticFiles();    // habilita wwwroot

// 👉 Fallback: si no encuentra ruta, devuelve el index.html de Pages
app.MapFallbackToFile("Pages/Login_page.html");

// Mapear controladores (API)
app.MapControllers();

app.Run();
