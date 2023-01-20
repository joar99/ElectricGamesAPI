using GameApiTwo.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameApiTwo.Models
{
    public class Developer : IDeveloper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        public string Location { get; set; } = "Not Specified";

        public string Image { get; set; } = "Not Specified";


    }
}
