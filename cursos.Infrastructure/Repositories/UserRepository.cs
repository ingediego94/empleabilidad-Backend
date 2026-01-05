using cursos.Domain.Entities;
using cursos.Domain.Interfaces;
using cursos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace cursos.Infrastructure.Repositories;

public class UserRepository : IGeneralRepository<User>
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    // --------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    
    // GET BY ID:
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    
    // CREATE:
    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    
    // UPDATE:
    public async Task<User?> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}