using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WSApp.Src.Domain.Entities.Base.Abstraction;

namespace WSApp.Src.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
