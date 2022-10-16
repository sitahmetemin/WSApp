namespace WSApp.Src.Application.Options.MongoDB.Abstraction
{
    public interface IMongoStoreDatabaseOption
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
