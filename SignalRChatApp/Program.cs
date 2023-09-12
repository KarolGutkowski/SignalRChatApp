using SignalRChatApp.Hubs;
using SignalRChatApp.Models;
using SignalRChatApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IChatRepository, ChatRepository>();

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

app.MapPost("chathub/{user}/newmessage", (string userName, string message, IChatRepository auctionRepo) =>
{
    var chatMessage = new ChatMessage(userName, message);
    auctionRepo.addMessage(chatMessage);
});

app.Run();
