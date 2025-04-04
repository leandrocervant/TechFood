using System.Text.Json.Serialization;
using TechFood.Application;
using TechFood.Application.Common.Filters;
using TechFood.Application.Common.NamingPolicy;
using TechFood.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
    })

    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })

    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new UpperCaseNamingPolicy()));
        options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
    });

    builder.Services.AddCors();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    builder.Services.AddIoCServices();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseApplication();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}