using Microsoft.OpenApi.Models;
using PipeManager.Client.Services.Abstractions;
using PipeManager.Client.Services.Implementations;
using PipeManager.Components;
using PipeService;
using PipeService.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.PipeServices(builder.Configuration);

//builder.Services.AddScoped<IPipeApiClient, PipeApiClient>();

string? apiBaseUrl = builder.Configuration["ApiBaseUrl"];
builder.Services.AddHttpClient<IPipeApiClient, PipeApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//builder.Services.AddInteractiveServerComponents();
//builder.Services.AddInteractiveServerRenderMode();

// --- Добавляем сервисы CORS ---
builder.Services.AddCors(options =>
{
    // Определяем политику CORS с именем "AllowAll"
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Разрешаем запросы с любого источника
              .AllowAnyMethod() // Разрешаем любые HTTP-методы (GET, POST, PUT и т.д.)
              .AllowAnyHeader(); // Разрешаем любые заголовки
    });
});


builder.Services.AddEndpointsApiExplorer();
// Добавляем генератор Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Определяем документ Swagger (версия v1)
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PipeManager API", Version = "v1" });
    // Включаем поддержку аннотаций (например, [SwaggerOperation]) в контроллерах
    c.EnableAnnotations();
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Middleware для генерации swagger.json
    app.UseSwagger();
    // Middleware для Swagger UI (интерфейса)
    app.UseSwaggerUI(c =>
    {
        // Устанавливаем конечную точку для файла swagger.json
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PipeManager API V1");
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

// --- Включаем Middleware CORS ---
// Используем ранее определенную политику "AllowAll"
app.UseCors("AllowAll");

app.UseAntiforgery();

app.MapControllers();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PipeManager.Client._Imports).Assembly);

app.Run();
