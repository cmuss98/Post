namespace Domain;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }

    public User User { get; set; }
}