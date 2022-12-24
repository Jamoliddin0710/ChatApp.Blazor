using ChatApp.Api;
using ChatApp.Api.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<IdentityDbContext>();
builder.Services.AddDbContext<IdentityDbContext>(options => {
    options.UseInMemoryDatabase("ChatApp");
});
builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(MessageService));
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policy =>
{
    policy.AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader();
});
});

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // authorize attributini ishlatib beradi
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.Run();
