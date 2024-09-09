using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProveedor
{
    /// <summary>
    /// Retrieves all active Proveedor entities, including their related location entities.
    /// </summary>
    /// <returns>ICollection of Proveedor</returns>
    Task<ICollection<Proveedor>> ListAllAsync();

    /// <summary>
    /// Retrieves all active Proveedor entities as an IQueryable for deferred execution.
    /// </summary>
    /// <returns>An IQueryable of active Proveedor entities.</returns>
    IQueryable<Proveedor> ListAllQueryable();

    /// <summary>
    ///  Retrieves a Proveedor entity by its ID, including related Contactos and location entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Proveedor if founded, otherwise null</returns>
    Task<Proveedor?> FindByIdAsync(byte id);

    /// <summary>
    /// Checks if a Proveedor with the specified ID exists and is active.
    /// </summary>
    /// <param name="id">The unique identifier of the Proveedor.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsProveedorAsync(byte id);

    /// <summary>
    /// Creates a new Proveedor entity in the database.
    /// </summary>
    /// <param name="proveedor">The Proveedor entity to be created.</param>
    /// <returns>Proveedor</returns>
    Task<Proveedor> CreateProveedorAsync(Proveedor proveedor);

    /// <summary>
    /// Updates an existing Proveedor entity in the database.
    /// </summary>
    /// <param name="proveedor">The Proveedor entity with updated information.</param>
    /// <returns>Proveedor</returns>
    Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor);

    /// <summary>
    /// Marks a Proveedor entity as inactive instead of deleting it from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the Proveedor to be marked as inactive.</param>
    /// <returns>The unique identifier of the Inventory to delete.</</returns>
    Task<bool> DeleteProveedorAsync(byte id);
}