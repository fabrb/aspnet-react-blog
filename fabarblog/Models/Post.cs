public class Post
{
	public required int Id { get; set; }
	public required string Title { get; set; }
	public required string Content { get; set; }

	public required int UserId { get; set; }
	public required User User { get; set; }

	public DateTime CreatedAt { get; set; }
}
