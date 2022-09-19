using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MStatistics.Client;
using MStatistics.Data;
using MStatistics.Data.Repositories;
using MStatistics.DomainModels;
using MStatistics.WebApi;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("TweetStatistics"));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITweetRepository, TweetRepository>();
builder.Services.AddTransient<IHashTagRepository, HashTagRepository>();
builder.Services.AddHttpClient<ITwitterService, TwitterService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["TwitterServiceBaseUrl"]);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["TwitterServiceBearerToken"]}");
})
    .AddPolicyHandler(GetRetryPolicy())
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
builder.Services.AddSingleton<TwitterListener>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<TwitterListener>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}