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

	public async Task<Post?> SearchPost(int id)
	{
		return await _context.Posts
			.Include(u => u.User)
			.FirstOrDefaultAsync(post => post.Id == id);
	}

	public async Task<Post> IncludeNewPost(Post post)
	{
		Post x = new();

		try
		{
			var entityEntry = await _context.Posts.AddAsync(post);
			await _context.SaveChangesAsync();

			x = entityEntry.Entity;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		return x;
	}

	public async Task<Post> EditPost(Post post)
	{
		try
		{
			_context.Posts.Entry(post).State = EntityState.Modified;
			await _context.SaveChangesAsync();

		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		return post;
	}

	public async Task<bool> RemovePost(Post post)
	{
		try
		{
			var x = _context.Posts.Remove(post);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			return false;
		}

		return true;
	}
}
