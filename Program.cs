using Microsoft.EntityFrameworkCore;
using ProfitCalcApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// SQLite の登録（ファイル名: profitcalc.db）
builder.Services.AddDbContext<AppDbContext>(options => //☆ここです☆
    options.UseSqlite("Data Source=profitcalc.db"));

var app = builder.Build();// --- ここからGitHubでデータベース動かすためのコード ---
    using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>(); // ※お使いのDbContext名に合わせてください　☆のところ
        context.Database.EnsureCreated(); // データベースを作る
    }
    catch (Exception ex)
    {
        // エラーが出ても止まらないようにログだけ出させる
        Console.WriteLine("DBの初期化に失敗しました: " + ex.Message);
    }
}// --- ここまで ---

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
