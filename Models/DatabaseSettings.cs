namespace GamesApi.Models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string GamesCollectionName { get; set; } = null!;

        public string CharactersCollectionName { get; set; } = null!;   

        public string DevelopersCollectionName { get; set; } = null!;

    }
}
