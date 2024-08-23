using AppData.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Server.Data;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Register HttpClient and services with specific API endpoints

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("https://localhost:7170")
});

// Register other services similarly if needed
builder.Services.AddScoped<IGenericService<Major>, GenericService<Major>>();
builder.Services.AddScoped<IGenericService<staff>, GenericService<staff>>();
builder.Services.AddScoped<IGenericService<StaffMajorFacility>, GenericService<StaffMajorFacility>>();
builder.Services.AddScoped<IGenericService<MajorFacility>, GenericService<MajorFacility>>();
builder.Services.AddScoped<IGenericService<DepartmentFacility>, GenericService<DepartmentFacility>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();
builder.Services.AddScoped<IGenericService<Facility>, GenericService<Facility>>();

// Build the app
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
