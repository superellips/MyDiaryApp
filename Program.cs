using MyDiaryApp;
using MyDiaryApp.Configuration;
using MyDiaryApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration settings to the container

builder.Services.Configure<MongoDbEntrySettings>(builder.Configuration.GetSection("MongoDbEntrySettings"));

// Add services to the container.
builder.Services.AddRazorPages();

switch (builder.Configuration.GetValue<string>("EntryServiceImplementation"))
{
    case "MongoDb":
        builder.Services.AddSingleton<IEntryDbService, MongoDbEntryService>();
        break;
    case "InMem":
    default:
        builder.Services.AddSingleton<IEntryDbService, InMemEntryDbService>();
        break;
}



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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
