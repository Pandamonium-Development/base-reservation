
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUsuario(BaseReservationContext context) : IRepositoryUsuario
{
    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    public async Task<Usuario?> FindByIdAsync(short id)
    {
        return await context.Set<Usuario>().FindAsync(id);
    }

    /// <summary>
    /// Get user with specific email
    /// </summary>
    /// <param name="correoElectronico">User's email</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    public async Task<Usuario?> FindByEmailAsync(string correoElectronico)
    {
        return await context.Set<Usuario>().Include(m => m.IdRolNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.CorreoElectronico == correoElectronico);
    }

    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>ICollection of Usuario</returns>
    public async Task<ICollection<Usuario>> ListAllAsync()
    {
        var collection = await context.Set<Usuario>()
            .Include(a => a.IdRolNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all users assigned to an specific role
    /// </summary>
    /// <param name="idRol">Role id</param>
    /// <returns>ICollection Usuario</returns>
    public async Task<ICollection<Usuario>> ListAllByRoleAsync(byte idRol)
    {
        var collection = await context.Set<Usuario>()
            .Include(a => a.IdRolNavigation)
            .AsNoTracking()
            .Where(m => m.IdRol == idRol)
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get user that can be logged in into system
    /// </summary>
    /// <param name="correoElectronico">User's email</param>
    /// <param name="contrasenna">User's password encrypted</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    public async Task<Usuario?> LoginAsync(string correoElectronico, string contrasenna)
    {
        return await context.Set<Usuario>().Include(m => m.IdRolNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.CorreoElectronico == correoElectronico && m.Contrasenna == contrasenna);
    }

    /// <summary>
    /// Validat if the user can be assigned to another branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="idSucursalAsignacion">Branch to be assigned</param>
    /// <returns>True if is available, if not, false</returns>
    public async Task<bool> IsAvailableAsync(short id, byte idSucursalAsignacion) => !await context.Set<UsuarioSucursal>().AsNoTracking().AnyAsync(m => m.IdUsuario == id && m.IdSucursal != idSucursalAsignacion);

    /// <summary>
    /// Validate if the user already exists
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsUsuarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Usuario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Usuario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}