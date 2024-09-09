
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUsuario(BaseReservationContext context) : IRepositoryUsuario
{
    /// <inheritdoc />
    public async Task<Usuario?> FindByIdAsync(short id)
    {
        return await context.Set<Usuario>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<Usuario?> FindByEmailAsync(string correoElectronico)
    {
        return await context.Set<Usuario>().Include(m => m.IdRolNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.CorreoElectronico == correoElectronico);
    }

    /// <inheritdoc />
    public async Task<ICollection<Usuario>> ListAllAsync()
    {
        var collection = await context.Set<Usuario>()
            .Include(a => a.IdRolNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Usuario>> ListAllByRoleAsync(byte idRol)
    {
        var collection = await context.Set<Usuario>()
            .Include(a => a.IdRolNavigation)
            .AsNoTracking()
            .Where(m => m.IdRol == idRol)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Usuario?> LoginAsync(string correoElectronico, string contrasenna)
    {
        return await context.Set<Usuario>().Include(m => m.IdRolNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.CorreoElectronico == correoElectronico && m.Contrasenna == contrasenna);
    }

    /// <inheritdoc />
    public async Task<bool> IsAvailableAsync(short id, byte idSucursalAsignacion) => !await context.Set<UsuarioSucursal>().AsNoTracking().AnyAsync(m => m.IdUsuario == id && m.IdSucursal != idSucursalAsignacion);

    /// <inheritdoc />
    public async Task<bool> ExistsUsuarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Usuario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Usuario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}