using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfR11nSHZTfkBqXX9ddw==;Mgo+DSMBPh8sVXJ1S0R+X1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTH5Xd01jUXpXdHZWQQ==;ORg4AjUWIQA/Gnt2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtTckRrWXdbeXBRTmc=;MTkyNzE1MEAzMjMxMmUzMjJlMzNQWU5vbFZZNFRtYmFIV000VUNDUEV3RXhiWnJ3d1Q4eHlodWlldHllMStZPQ==;MTkyNzE1MUAzMjMxMmUzMjJlMzNCKzRNNENQV1lMNXhSY2dPSmZWdEdYUVVVcnBoNDNvelhBV2Z5MlBveDlJPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5WdkFiUX9WdX1QRGBb;MTkyNzE1M0AzMjMxMmUzMjJlMzNPM1ZaVWgwY0h6YkFDWEFXK1MzdXJ0U0liRTZGTXBvYUVFUXFXcWhNaVdjPQ==;MTkyNzE1NEAzMjMxMmUzMjJlMzNLQ2ZaNlFhcUtTb1Bzc3Evd20wOWpuVDJhQ3h6dW9aRTN3RzRSbjhTbFQwPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtTckRrWXdbeXFcR2c=;MTkyNzE1NkAzMjMxMmUzMjJlMzNUT21qQ3ZJV3A4eEdqb012UnFTMlQ4V0lxQ0NXME1MKzc5MkduNCtTTXBVPQ==;MTkyNzE1N0AzMjMxMmUzMjJlMzNPakRxOEV1M3BKWEFCMDZMQ2x3d1pVaGRWcUVzQ2JMY0ErSDNnaGp3N3JVPQ==;MTkyNzE1OEAzMjMxMmUzMjJlMzNPM1ZaVWgwY0h6YkFDWEFXK1MzdXJ0U0liRTZGTXBvYUVFUXFXcWhNaVdjPQ==");

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Dependency Injections.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
