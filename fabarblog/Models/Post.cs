namespace fabarblog.Models;
public class Post
{
	public required Guid Id { get; set; }
	public required string Title { get; set; }
	public required string Content { get; set; }

	public Guid UserId { get; set; }
	public User User { get; set; }

	public DateTime CreatedAt { get; set; }
}
