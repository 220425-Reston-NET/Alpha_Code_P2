using MedTrakBL;
using MedTrakDL;
using MedTrakModel;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<User>, SqlUserRepository>(repo => new SqlUserRepository(Environment.GetEnvironmentVariable("Connection_String")));
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IRepository<Medicine>, SqlMedicineRepository>(repo => new SqlMedicineRepository(Environment.GetEnvironmentVariable("Connection_String")));
builder.Services.AddScoped<IMedicineBL, MedicineBL>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
