using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalRChatApp.Hubs;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR((hubOptions) =>
{
    hubOptions.EnableDetailedErrors = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IChatRepository, ChatRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalHostConnections",
        builder=>
        {
            builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});


var app = builder.Build();
app.UseCors("AllowLocalHostConnections");

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
app.MapDefaultControllerRoute();
app.MapHub<ChatHub>("/chathub");

app.MapPost("chathub/{userName}/newmessage",
    async ([FromRoute] string userName,
    [FromBody] MessageDto dto,
    IChatRepository auctionRepo/*,
    IHubContext<ChatHub> hubContext*/) =>
{
    var chatMessage = new ChatMessage(userName, dto.message);
    auctionRepo.addMessage(chatMessage);
    // await hubContext.Clients.All.SendAsync("ReceiveMessage", chatMessage);
});


app.Run();
