using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SignalRChatApp.Hubs;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IChatRepository, ChatRepository>();

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
app.MapDefaultControllerRoute();
app.MapHub<ChatHub>("/chathub");

app.MapPost("chathub/{userName}/newmessage", 
    ([FromRoute] string userName, 
    [FromBody] MessageDto dto, 
    IChatRepository auctionRepo) =>
{
    var chatMessage = new ChatMessage(userName, dto.message);
    auctionRepo.addMessage(chatMessage);
});

app.Run();
