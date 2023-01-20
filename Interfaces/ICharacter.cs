using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameApiTwo.Interfaces
{
    public interface ICharacter
    {

        public string? Id { get; set; }

        public string Name { get; set; }

        public string Game { get; set; }

        public string Image { get; set; }

        public string Weapon { get; set; }


    }

}

