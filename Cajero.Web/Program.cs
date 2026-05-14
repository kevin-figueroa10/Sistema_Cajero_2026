using Cajero.Core.Interfaces;
using Cajero.Core.Repositories;
using Cajero.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Inyección de dependencias
builder.Services.AddScoped<IRepositorioCuenta, RepositorioCuenta>();
builder.Services.AddScoped<IRepositorioTransaccion, RepositorioTransaccion>();
builder.Services.AddScoped<IServicioCajero, ServicioCajero>();

// Agregar servicios MVC
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=Index}/{id?}");

app.Run();
