using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using fabarblog.Models;
using fabarblog.Data;

namespace fabarblog.Repository;
public class UserRepository
{
	private readonly Context _context;

	public UserRepository(Context context)
	{
		_context = context;
	}

	public async Task<IEnumerable<User>> SearchAllUsers()
	{
		return await _context.Users
			.Include(u => u.Posts)
			.ToListAsync();
	}

	public async Task<User?> SearchUser(int id)
	{
		return await _context.Users
			.FirstOrDefaultAsync(user => user.Id == id);
	}

	public async Task<User?> SearchUserByEmail(string email)
	{
		return await _context.Users
			.FirstOrDefaultAsync(user => user.Email == email);
	}

	public async Task<bool> IncludeNewUser(User user)
	{
		try
		{
			var x = await _context.Users.AddAsync(user);
			Console.WriteLine(x);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine("IncludeNewUser ERROR");
			Console.WriteLine(ex);
		}

		return true;
	}

	public async Task<bool> EditUser(User user)
	{
		try
		{
			var x = _context.Users.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		return true;
	}

	public async Task<bool> Removeuser(User user)
	{
		try
		{
			var x = _context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		return true;
	}
}
