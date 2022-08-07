using EmailService;
using IdentityByExamples.CustomTokenProviders;
using IdentityByExamples.Factory;
using IdentityByExamples.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Install Microsoft.AspNetCore.Mvc.NewtonsoftJson to avoid looping in the related entities when showing json
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(opt =>

    opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))
    );

builder.Services.AddIdentity<User, IdentityRole>
    (
        opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireUppercase = true;
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = true;
            opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
        }
    )
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromDays(3));

builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Authentication/Login");
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
  .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
