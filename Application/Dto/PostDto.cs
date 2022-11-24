namespace Application.Dto;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    
    public string UserName { get; set; }
}