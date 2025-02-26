using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MOFA.StockManagement.ApplicationService.AppService.Extension;
using MOFA.StockManagement.ApplicationService.AppService;
using MOFA.StockManagement.ApplicationService.ViewModels;
using MOFA.StockManagement.Presentation.UI;
using MOFA.StockManagement.Presentation.UI.Resources;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services
    .AddRazorPages()
    //.AddRazorPagesOptions(opts =>
    //{
    //    opts.Conventions.AuthorizePage("/Index");
    //    //opts.Conventions.AuthorizeAreaFolder("IdentityArea", "/");
    //    //opts.Conventions.AuthorizeAreaFolder("ClientsArea", "/");
    //    //opts.Conventions.AuthorizeAreaFolder("ProductArea", "/");
    //})
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(opts =>
    {
        opts.DataAnnotationLocalizerProvider =
        (type, factory) => factory.Create(nameof(SharedResource), new System.Reflection.AssemblyName(typeof(SharedResource).Assembly.FullName!).Name!);
    })
    .AddMvcOptions(options =>
    {
        //https://github.com/KuniyoshiKamimura/IValidationAttributeAdapterProviderSample/
        options.ModelMetadataDetailsProviders.Add(new CustomValidationMetadataProvider());

        static string f1(string f, string a1) => string.Format(f, a1);
        static string f2(string f, string a1, string a2) => string.Format(f, a1, a2);

        var Localizer = builder.Services?.BuildServiceProvider().GetService<IStringLocalizerFactory>()?.Create(nameof(SharedResource), new System.Reflection.AssemblyName(typeof(SharedResource).Assembly.FullName!).Name!);

        var mp = options.ModelBindingMessageProvider;
        mp.SetAttemptedValueIsInvalidAccessor((x, y) => f2(Localizer!["ModelBinding_AttemptedValueIsInvalid"], x, y));
        mp.SetMissingBindRequiredValueAccessor((x) => f1(Localizer!["ModelBinding_MissingBindRequiredValue"], x));
        mp.SetMissingKeyOrValueAccessor(() => Localizer!["ModelBinding_MissingKeyOrValue"]);
        mp.SetMissingRequestBodyRequiredValueAccessor(() => Localizer!["ModelBinding_MissingRequestBodyRequiredValue"]);
        mp.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => f1(Localizer!["ModelBinding_NonPropertyAttemptedValueIsInvalid"], x));
        mp.SetNonPropertyUnknownValueIsInvalidAccessor(() => Localizer!["ModelBinding_NonPropertyUnknownValueIsInvalid"]);
        mp.SetNonPropertyValueMustBeANumberAccessor(() => Localizer!["ModelBinding_NonPropertyValueMustBeANumber"]);
        mp.SetUnknownValueIsInvalidAccessor((x) => f1(Localizer!["ModelBinding_UnknownValueIsInvalid"], x));
        mp.SetValueIsInvalidAccessor((x) => f1(Localizer!["ModelBinding_ValueIsInvalid"], x));
        mp.SetValueMustBeANumberAccessor((x) => f1(Localizer!["ModelBinding_ValueMustBeANumber"], x));
        mp.SetValueMustNotBeNullAccessor((x) => f1(Localizer!["ModelBinding_ValueMustNotBeNull"], x));
    })
    ;

builder.Services.AddSession();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>(),
//         ValidAudience = builder.Configuration.GetSection("Jwt:Audiance").Get<string>(),
//         ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Get<string>())),
//     };
// });


builder.Services
    .AddHttpContextAccessor();


builder.Services.AddTransient<ProtectedApiBearerTokenHandler>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services
    .AddHttpClient("Service", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetSection("Service").Value!);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    })
    .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();

builder.Services
    .AddHttpClient("IdService", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetSection("IdService").Value!);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    })
    .AddHttpMessageHandler<ProtectedApiBearerTokenHandler>();


//builder.Services
//    .AddSingleton<IHttpClientHelper<LoginViewModel>, HttpClientIdentityServiceHelper<LoginViewModel>>()
//    ;





builder.Services
    .AddScoped<IHttpClientHelper<ItemTypeViewModel>, HttpClientServiceHelper<ItemTypeViewModel>>()
    .AddScoped<IHttpClientHelper<WarehouseViewModel>, HttpClientServiceHelper<WarehouseViewModel>>()
    .AddScoped<IHttpClientHelper<ItemViewModel>, HttpClientServiceHelper<ItemViewModel>>()
;

builder.Services
    .AddScoped<IItemTypeAppService, ItemTypeAppService>()
    .AddScoped<IWarehouseAppService, WarehouseAppService>()
    .AddScoped<IItemAppService, ItemAppService>()
    ;

builder.Services.AddRazorPages();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "ar" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    var questStringCultureProvider = options.RequestCultureProviders[0];
    options.RequestCultureProviders.RemoveAt(0);
    options.RequestCultureProviders.Insert(1, questStringCultureProvider);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        if (string.IsNullOrEmpty(context.HttpContext.Request.Path.Value))
            response.Redirect("/Login");
        else
            response.Redirect($"/Login?returnUrl={context.HttpContext.Request.Path.Value}");
    }

    if (response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        response.Redirect("/Login");
    }

    return Task.CompletedTask;
});

app.UseSession();

//app.Use(async (context, next) =>
//{
//    var token = context.Session.GetString("Token");
//    var refToken = context.Session.GetString("RefToken");
//    if (!string.IsNullOrEmpty(token))
//    {
//        using (var scope = app.Services.CreateScope())
//        {
//            var idser = scope.ServiceProvider.GetRequiredService<IIdentityAppService>();
//            try
//            {
//                var authmodel = await idser.RefreshTokenAsync(refToken);
//                if (authmodel is not null && authmodel.Succeeded)
//                {
//                    context.Session.SetString("Token", authmodel!.AccessToken!);
//                    context.Session.SetString("RefToken", authmodel!.RefreshToken!);
//                    token = authmodel!.AccessToken!;
//                }
//            }
//            catch (Exception e)
//            {
//                // swallow
//            }
//        }
//        context.Request.Headers.Add("Authorization", "Bearer " + token);
//    }
//    await next();
//});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions!.Value);

//app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
