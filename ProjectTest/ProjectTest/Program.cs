using Microsoft.EntityFrameworkCore;
using ProjectTest.Data;
using ProjectTest.Repo.Interface;
using ProjectTest.Repo;
using ProjectTest.Services.Interface;
using ProjectTest.Services;
using ProjectTest.Tool;
using ProjectTest.Tool.ServicesTool.IServicesTool;
using ProjectTest.Tool.ServicesTool;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnectionString")));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISendMailService, SendMailService>();
builder.Services.AddScoped<IDataEmailRepo, DataEmailRepo>();

builder.Services.AddSingleton<IWorker, Worker>();
builder.Services.AddHostedService<DerivedBackgroundPrinter>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
