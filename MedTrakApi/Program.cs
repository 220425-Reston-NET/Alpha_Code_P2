using MedTrakBL;
using MedTrakDL;
using MedTrakModel;


var builder = WebApplication.CreateBuilder(args);

// Adding CORS to allow all origins to have acess to our backend
builder.Services.AddCors(
    (options) => {
        options.AddDefaultPolicy(origin => {

            origin.AllowAnyOrigin(); //Allows any origin to talk to your backend
            origin.AllowAnyMethod(); //Allows any http verb request in our backend
            origin.AllowAnyHeader(); //Allows any http headers to have access to my backend
        });
    }
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Configuration.GetConnectionString("Israel ngandu")

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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
