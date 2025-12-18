using Employee_Blazor_machineTest.Data;
using Employee_Blazor_machineTest.Helper;
using Employee_Blazor_machineTest.Model;
using Employee_Blazor_machineTest.Repository;
using Employee_Blazor_machineTest.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();



builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
