using AlisverisProje.Identity;
using AlisverisProje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Veritabanı Connection Kodumuz ve AddDbContext Veritabanını Tanımlama. Ve Veri Atma Kodlarımız
    var connection = builder.Configuration["Ayarlar:BaglantiSatirim"];
    builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connection));

#endregion

#region Identity User,Role ve DbContext Yapılandırması ve kullanıcı denetimi 

builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();  

//Şifre kurallarını kullanıcılar için tanımlıyoruz. Kuralları geliştirmek
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //şifresinde sayılar zorunlu olsun
    options.Password.RequireNonAlphanumeric = true; //şifresinde yazı karakterleri zorunlu olsun
    options.Password.RequireLowercase = true;  //şifresinde küçük harf zorunlu olsun
    options.Password.RequiredLength = 6; //minimum 6 karakter olsun
    options.Password.RequireUppercase = true; //şifresinde büyük harfte zorunlu olsun

    options.Lockout.MaxFailedAccessAttempts = 5;// 5 kez yanlış girilirse uzaklaştır
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // yanlış girilirse 6 dk uzaklaştır
    options.Lockout.AllowedForNewUsers = true;//Yeni kullanıcılar içinde bu uzaklaştırma geçerli.

    options.User.RequireUniqueEmail = true;//Mail ile kayıt olma işlemi bir mail 1 kez kullanılabilir
    options.SignIn.RequireConfirmedEmail = true;//Mail adresine onaylama gidecek ve onaylama işlemi yapılırsa üyeliği aktif edilecek.
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon numarasıda doğrulanmış ise giriş yapılabilecek. //False dedik gerek yok telefon onaylamaya
});

#endregion

#region Cookie Ayarlarının yapılandırılması

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Guvenlik/Giris"; // Kullanıcının login olacağı  sayfa 
    options.LogoutPath = "/Guvenlik/Cikis"; // Kişinin çıkış yapması için 
    options.AccessDeniedPath = "/Guvenlik/ErisimYetkinizYok"; //kişinin erişim yetkisi yoksa yönlendirilecek sayfa.
    options.SlidingExpiration = true; //Cookie default 20 dk ise 15. dkda sisteme tekkrar girerse20 dk yenilenir
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true, //client scripti vasıtasıylada erişim sağlanabilir.
        Name = ".AdonetCore.Guvenlik.Cookie",
        Path = "/",
        //SameSite=SameSiteMode.Strict// Uygulama dışından erişimi cookie isteği bulunmasını engelliyoruz.
        SameSite = SameSiteMode.Lax// yapılan istekle aynı domainde ise çalışabilecek olacak şekilde gelirse yine kullanılabilir.
    };
});

#endregion

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

#region Giriş İşlemi
    app.UseAuthentication();
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
