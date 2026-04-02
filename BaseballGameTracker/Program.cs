
using BaseballGameTracker.Data;
using BaseballGameTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BaseballGameTracker.MappingProfiles;
using BaseballGameTracker.Application.Services; 



    using Serilog;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Host.UseSerilog((ctx, config) => 
    config.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration)
  );

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA2NTM3NjAwIiwiaWF0IjoiMTc3NTAwNTEzMiIsImFjY291bnRfaWQiOiIwMTlkNDY4YmYzZmE3ZWQ1YjFiM2JkMDI4NzI1ODg0YiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa24zOHNzOGpqcDFlaDlwMGFqd21xZTAzIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.X48ALQZu_rnMZVO6eao4VvAwhYtYGnO9nuMfeO1yKb83RO4z5j6t0Jd91nyY1v70svCsJtjDE5GCIVh813DaOtx4UBg1Ssfqae063OU5CTM6oewLm2r9lylNwttPDiDZgR5b5ZH-2YIjDthPWoaY9GkLM9GzqrD3m4ClsyhyTGsIsWuIs-9pbqm9BY7NB7fXrDL6t4NCdTiz6mbrY2yllhcTF-Dw8QuKVnDWkT_F43P_rCwrWDQQWqU3rRYlO84HFteKkouVt0_3HQgqx2trGmjmz8HsIS-tltjKjaxQMD66xkT-0u6AervgHcqVl8myRureVvn3IZ15rnKXsarycA";
});


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();



builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IEmailSenderService,EmailSenderService>();



builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new GameAutoMapperProfile());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Schedule Job
ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
IScheduler schedule = await schedulerFactory.GetScheduler(); 

await schedule.Start();

IJobDetail job = JobBuilder.Create<JobService>()
    .WithIdentity("jobService", "group1")
    .Build();

//ITrigger trigger = TriggerBuilder.Create()
//    .WithIdentity("jobService", "group1")
//    .StartNow()
//    .WithSimpleSchedule(x =>
//        x.WithIntervalInMinutes(20).RepeatForever())
//    .Build();

// Every Morning at 5am send email.
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("jobService", "group1")
    .StartNow()
    .WithCronSchedule("0 0 5 * * ?")
    .Build();


//.WithCronSchedule("0 0 5 * * ?")

await schedule.ScheduleJob(job, trigger); 



app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
