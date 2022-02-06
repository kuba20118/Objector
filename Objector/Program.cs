using Microsoft.Extensions.ML;
using NetCore.AutoRegisterDi;
using Objector.Extensions;
using Objector.ML.Config;
using Objector.ML.DataModels;
using Objector.Mongo;
using Objector.Repositories;
using Objector.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterAssemblyPublicNonGenericClasses(typeof(ObjectDetectionService).Assembly)
    .Where(x => x.Name.EndsWith("Service"))
    .AsPublicImplementedInterfaces();

builder.Services.RegisterAssemblyPublicNonGenericClasses(typeof(ImagesRepository).Assembly)
    .Where(x => x.Name.EndsWith("Repository"))
    .AsPublicImplementedInterfaces();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));

var onnxModelConfigurator = new OnnxModelConfigurator(new TinyYoloModel(PathExtensions.GetAbsolutePath("ONNX/OnnxModels/TinyYolo2_model.onnx")));
onnxModelConfigurator.SaveMLNetModel(PathExtensions.GetAbsolutePath("ONNX/OnnxModels/TinyYoloModel.zip"));

builder.Services.AddPredictionEnginePool<ImageInputData, TinyYoloPrediction>().FromFile(PathExtensions.GetAbsolutePath("ONNX/OnnxModels/TinyYoloModel.zip"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyOrigin();
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
