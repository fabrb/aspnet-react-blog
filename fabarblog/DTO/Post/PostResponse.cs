namespace fabarblog.DTO;
public class Author
{
	public int Id { get; set; }
	public string Name { get; set; }

}

public class PostResponse
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public Author? Author { get; set; }
	public string? Content { get; set; }
	public DateTime CreationDate { get; set; }
}
