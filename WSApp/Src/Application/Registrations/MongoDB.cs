using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WSApp.Src.Application.Options.MongoDB;
using WSApp.Src.Application.Options.MongoDB.Abstraction;

namespace WSApp.Src.Application.Registrations
{
    public static class MongoDB
    {
        public static void AddMongoDB(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoStoreDatabaseOption>(builder.Configuration.GetSection(nameof(MongoStoreDatabaseOption)));

            builder.Services.AddSingleton<IMongoStoreDatabaseOption>(sp => sp.GetRequiredService<IOptions<MongoStoreDatabaseOption>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("MongoStoreDatabaseOption:ConnectionString")));
        }
    }
}
