using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class PostRepository
{
	private readonly Context _context;

	public PostRepository(Context context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Post>> SearchAllPosts()
	{
		return await _context.Posts.ToListAsync();
	}
}
