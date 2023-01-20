using GameApiTwo.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamesApi.Models;

public class Game : IGame
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    public string ReleaseDate { get; set; } = "Not Specified";

    public string Image { get; set; } = "defaultgame.jpg";

    public string Platform { get; set; } = "Not Specified";

    public string Developer { get; set; } = "Not Specified";

    public string ESRB { get; set; } = "Not Specified"; 

}
