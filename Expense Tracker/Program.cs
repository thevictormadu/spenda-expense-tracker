using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTg1MTMwN0AzMjMxMmUzMTJlMzMzNUNYSkozQ0xHV2w2MzJxRHlZN1Jpbm5oZnpTb1h1YkUrV3BQOENvdkkxUVE9;Mgo+DSMBaFt+QHFqVkNrWU5HaV1CX2BZfVl2TGlbe04QCV5EYF5SRHJdRFxmSXdRckRhUXo=;Mgo+DSMBMAY9C3t2VFhhQlJBfVpdXnxLflF1VWBTf1d6dlFWACFaRnZdQV1nS3hSc0VqW3tfdHdQ;Mgo+DSMBPh8sVXJ1S0d+X1RPc0BBQmFJfFBmRmlZdlRycEUmHVdTRHRcQllhT39WdkxhXX5YcHE=;MTg1MTMxMUAzMjMxMmUzMTJlMzMzNVBLU0dtbjc1LzRYNXd4M044cFVuMDBvVit1TFA3MGVTcXBoanFmbjJkWU09;NRAiBiAaIQQuGjN/V0d+XU9Hc1RHQmBWfFN0RnNadV55fldCcDwsT3RfQF5jTXxVd0BjUH1acnRXQw==;ORg4AjUWIQA/Gnt2VFhhQlJBfVpdXnxLflF1VWBTf1d6dlFWACFaRnZdQV1nS3hSc0VqW3tccnRQ;MTg1MTMxNEAzMjMxMmUzMTJlMzMzNWJtRm1vaG5KVC9KT3EvcnJaOWFjTHU3YkR3M3F2SXFISkdkUFBLYnVYWmM9;MTg1MTMxNUAzMjMxMmUzMTJlMzMzNUZERTR0akh5MHp6OTQySzJpRU1oN0tNWEREUEgvQWl6WDhQV1dIYjlzT289;MTg1MTMxNkAzMjMxMmUzMTJlMzMzNVROYkZYcktQM3pCYW9HSFNiNnN3b3prV3h3QUF6anJUbEQzelhtekdXTmc9;MTg1MTMxN0AzMjMxMmUzMTJlMzMzNWVqZW5ZRVhyQnVCbXA5QTVMR0lHbU4zNlI2VVVCeUpMS1dwL3lUQldCaUU9;MTg1MTMxOEAzMjMxMmUzMTJlMzMzNUNYSkozQ0xHV2w2MzJxRHlZN1Jpbm5oZnpTb1h1YkUrV3BQOENvdkkxUVE9");

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
