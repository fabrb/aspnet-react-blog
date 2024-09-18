using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using fabarblog.Models;
using fabarblog.Data;

namespace fabarblog.Repository;
public class PostRepository
{
	private readonly Context _context;

	public PostRepository(Context context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Post>> SearchAllPosts()
	{
		return await _context.Posts
			.Include(u => u.User)
			.ToListAsync();
	}

	public async Task<bool> IncludeNewPost(Post post)
	{
		try
		{
			var x = await _context.Posts.AddAsync(post);
			Console.WriteLine(x);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine("IncludeNewPost ERROR");
			Console.WriteLine(ex);
		}

		return true;
	}
}
