using MongoDB.Bson.Serialization.Attributes;
using WSApp.Src.Domain.Entities.Base;

namespace WSApp.Src.Domain.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("modelName")]
        public string ModelName { get; set; }

        [BsonElement("modelNo")]
        public string ModelNo { get; set; }

        [BsonElement("os")]
        public string OS { get; set; }

        [BsonElement("processorBrand")]
        public string ProcessorBrand { get; set; }

        [BsonElement("processorType")]
        public string ProcessorType { get; set; }

        [BsonElement("processorVersion")]
        public string ProcessorVersion { get; set; }

        [BsonElement("ram")]
        public string Ram { get; set; }

        [BsonElement("storageSize")]
        public string StorageSize { get; set; }

        [BsonElement("storageType")]
        public string StorageType { get; set; }

        [BsonElement("displaySize")]
        public string DisplaySize { get; set; }

        [BsonElement("score")]
        public string score { get; set; }

        [BsonElement("prices")]
        public string Prices { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("site")]
        public string Site { get; set; }
    }
}