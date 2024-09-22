using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace fabarblog.Models;
public class Post
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }

	public int UserId { get; set; }

	public User User { get; set; }

	public DateTime CreatedAt { get; set; }

}
