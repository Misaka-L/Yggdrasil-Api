using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Yggdrasil.Filters;
using Yggdrasil.Models.Options;
using Yggdrasil.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IYggdrasilConnecterService, YggdrasilConnecterService>();
builder.Services.AddTransient<YggdrasilService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 该部分代码仅作为开发测试使用，请勿模仿
builder.Services.AddSingleton<HttpClient>(_ => {
    var client = new HttpClient();
    client.BaseAddress = new Uri("http://localhost:5566/");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("YggdrasilApi", "0.0.1"));

    return client;
});

builder.Services.Configure<JwtOption>(
    builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<YggdrasilOption>(
    builder.Configuration.GetSection("Yggdrasil"));
builder.Services.AddDistributedMemoryCache();
builder.Services.Configure<MvcOptions>(options => {
    options.Filters.Add<ExceptionFilter>();
    options.Filters.Add<ActionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
