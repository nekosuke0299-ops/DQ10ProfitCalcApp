using Microsoft.EntityFrameworkCore;
using ProfitCalcApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// SQLite の登録（ファイル名: profitcalc.db）
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=profitcalc.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
