using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProveedor
{
    Task<ICollection<Proveedor>> ListAllAsync();

    IQueryable<Proveedor> ListAllQueryable();

    Task<Proveedor?> FindByIdAsync(byte id);

    Task<bool> ExistsProveedorAsync(byte id);

    Task<Proveedor> CreateProveedorAsync(Proveedor proveedor);

    Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor);

    Task<bool> DeleteProveedorAsync(byte id);
}