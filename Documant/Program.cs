using Documant.Context;
using Documant.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Tüm POST isteklerinde CSRF (AntiForgery) doğrulaması otomatik yapılır.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

    // Sayı alanlarına boş/hatalı değer girilince İngilizce yerine Türkçe mesaj gösterilir.
    var messages = options.ModelBindingMessageProvider;
    messages.SetValueMustNotBeNullAccessor(_ => "Bu alan zorunludur.");
    messages.SetValueIsInvalidAccessor(_ => "Girilen değer geçersiz.");
    messages.SetAttemptedValueIsInvalidAccessor((_, _) => "Girilen değer geçersiz.");
    messages.SetValueMustBeANumberAccessor(_ => "Lütfen sayısal bir değer giriniz.");
});

builder.Services.AddDbContext<PortfolioContext>();
builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();

// Boş tablolara örnek veri ekler (dolu tablolara dokunmaz)
using (var scope = app.Services.CreateScope())
{
    try
    {
        DbSeeder.Seed(scope.ServiceProvider.GetRequiredService<PortfolioContext>());
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Örnek veri eklenemedi (migration uygulandı mı?).");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Çalışma zamanında yüklenen görseller (wwwroot/uploads) için gerekli.
// MapStaticAssets yalnızca derleme zamanında bilinen dosyaları sunar.
app.UseStaticFiles();
app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
