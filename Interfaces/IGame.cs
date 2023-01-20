namespace GameApiTwo.Interfaces
{
    public interface IGame
    {
        string Id { get; set; }
        string Title { get; set; }
        string ReleaseDate { get; set; }
        string Image { get; set; }
        string Platform { get; set; }

        string Developer { get; set; }

        string ESRB { get; set; }   

    }
}
