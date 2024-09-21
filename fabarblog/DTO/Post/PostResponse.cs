namespace fabarblog.DTO;
public class PostResponse
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Author { get; set; }
	public string? Content { get; set; }
	public DateTime CreationDate { get; set; }
}
