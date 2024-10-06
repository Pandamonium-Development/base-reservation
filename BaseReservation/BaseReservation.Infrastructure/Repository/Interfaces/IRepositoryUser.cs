using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUser
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>ICollection of User</returns>
    Task<ICollection<User>> ListAllAsync();

    /// <summary>
    /// Get list of all users assigned to an specific role
    /// </summary>
    /// <param name="idRole">Role id</param>
    /// <returns>ICollection User</returns>
    Task<ICollection<User>> ListAllByRoleAsync(byte idRole);

    /// <summary>
    /// Validat if the user can be assigned to another branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch to be assigned</param>
    /// <returns>True if is available, if not, false</returns>
    Task<bool> IsAvailableAsync(short id, byte branchId);

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>User if founded, otherwise null</returns>
    Task<User?> FindByIdAsync(short id);

    /// <summary>
    /// Validate if the user already exists
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsUserAsync(short id);

    /// <summary>
    /// Get user with specific email
    /// </summary>
    /// <param name="email">User's email</param>
    /// <returns>User if founded, otherwise null</returns>
    Task<User?> FindByEmailAsync(string email);

    /// <summary>
    /// Get user that can be logged in into system
    /// </summary>
    /// <param name="email">User's email</param>
    /// <param name="password">User's password encrypted</param>
    /// <returns>User if founded, otherwise null</returns>
    Task<User?> LoginAsync(string email, string password);
}