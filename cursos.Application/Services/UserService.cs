using cursos.Application.Interfaces;
using cursos.Domain.Entities;
using cursos.Domain.Interfaces;

namespace cursos.Application.Services;

public class UserService : IUserService
{
    private readonly IGeneralRepository<User> _userRepository;

    public UserService(IGeneralRepository<User> userRepository)
    {
        _userRepository = userRepository; 
    }
    
    // ---------------------------------------------------
    
    // GET ALL:
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    
    // GET BY ID:
    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    
    // CREATE:
    public async Task<User> CreateAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    
    // UPDATE:
    public async Task<bool> UpdateAsync(User user)
    {
        var exists = await _userRepository.GetByIdAsync(user.Id);

        if (exists == null)
            return false;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    
    // DELETE:
    public async Task<bool> DeleteAsync(int id)
    {
        var toDelete = await _userRepository.GetByIdAsync(id);

        if (toDelete == null)
            return false;

        await _userRepository.DeleteAsync(toDelete);
        return true;
    }
}