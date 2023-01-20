using GameApiTwo.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamesApi.Models;

public class Character : ICharacter
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public string Game { get; set; } = "Not Specified";

    public string Image { get; set; } = "Not Specified";

    public string Weapon { get; set; } = "Not Specified";


}
