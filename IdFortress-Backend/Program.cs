using IdFortress.Application.Services.Contracts;
using IdFortress.Application.Services;
using IdFortress.Infrastructure.Config;
using IdFortress.Infrastructure.Context;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoSettings").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return sp.GetRequiredService<IMongoClient>().GetDatabase(settings.DataBaseName);
});

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IBiometriaFacialService, BiometriaFacialService>();
builder.Services.AddScoped<IBiometriaDigitalService, BiometriaDigitalService>();
builder.Services.AddScoped<IDocumentosService, DocumentosService>();
builder.Services.AddScoped<INotificacaoFraudeService, NotificacaoFraudeService>();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
