using WSApp.Src.Application.Options.MongoDB.Abstraction;

namespace WSApp.Src.Application.Options.MongoDB
{
    public class MongoStoreDatabaseOption : IMongoStoreDatabaseOption
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}