using MongoDB.Bson.Serialization.Attributes;

namespace AspNetCoreMongoDb.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CityId { get; set; }

        [BsonIgnore]
        public virtual City City { get; set; }
    }
}