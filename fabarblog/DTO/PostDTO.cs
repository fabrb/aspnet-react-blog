using System.Runtime.InteropServices;

namespace fabarblog.DTO;
public class PostDTO
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
}
