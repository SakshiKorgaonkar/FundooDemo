using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Interface;
using RepoLayer.Service;
using RepoLayer.Utility;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<PasswordHashing>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddScoped<INoteRL, NoteRL>();
builder.Services.AddScoped<INoteBL, NoteBL>();
builder.Services.AddScoped<ILabelBL, LabelBL>();
builder.Services.AddScoped<ILabelRL, LabelRL>();
builder.Services.AddScoped<ILabelNoteRL,LabelNoteRL>();
builder.Services.AddScoped<ILabelNoteBL,LabelNoteBL>();
builder.Services.AddScoped<ICollaboratorBL, CollaboratorBL>();
builder.Services.AddScoped<ICollaboratorRL, CollaboratorRL>();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
builder.Services.AddDbContext<ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();