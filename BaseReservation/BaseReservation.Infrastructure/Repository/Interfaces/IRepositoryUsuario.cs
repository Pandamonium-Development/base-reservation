using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUsuario
{
    Task<ICollection<Usuario>> ListAllAsync();

    Task<ICollection<Usuario>> ListAllByRoleAsync(byte idRol);

    Task<bool> IsAvailableAsync(short id, byte idSucursalAsignacion);

    Task<Usuario?> FindByIdAsync(short id);

    Task<bool> ExistsUsuarioAsync(short id);

    Task<Usuario?> FindByEmailAsync(string correoElectronico);

    Task<Usuario?> LoginAsync(string correoElectronico, string contrasenna);
}