using TallerTECService.Data;

IronPdf.License.LicenseKey =
                "IRONPDF.KEVINBARRANTESCERDAS.879-F95BAADBEC-DQP7PME7TJLXNHV-74QQER3IHGYM-7XI4HD3SDNMA-NV2FWOKZFMQT-42ZDIABO55TN-SDLYZA-THUAW2COXZ2HUA-DEPLOYMENT.TRIAL-7FBTQF.TRIAL.EXPIRES.14.OCT.2022";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Utilizamos injeccion de dependencias para mapear ILoginRepo a MockLoginRepo
//AddScoped crea una instancia de MockLoginRepo por request haciendo uso del
//constructor en LoginsController
builder.Services.AddScoped<ITallerRepo, JsonTallerRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
    policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
