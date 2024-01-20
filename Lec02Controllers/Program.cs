var builder = WebApplication.CreateBuilder(args);

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

// Variable number of segments
app.MapControllerRoute(
    name: "variable",
    pattern: "query/{name}/{*values}",
    defaults: new { controller = "Home", action = "Variable" });
// Custom route
app.MapControllerRoute(
    name: "student",
    pattern: "student/{enumber}",
    defaults: new { controller = "Home", action = "Details" });
// Three segments
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Four segments
app.MapControllerRoute(
    name: "fourSegments",
    pattern: "{controller}/{action}/{code}/{idnumber}");

app.Run();
