using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryVendor
{
    /// <summary>
    /// Retrieves all active Vendor entities, including their related location entities.
    /// </summary>
    /// <returns>ICollection of Vendor</returns>
    Task<ICollection<Vendor>> ListAllAsync();

    /// <summary>
    /// Retrieves all active Vendor entities as an IQueryable for deferred execution.
    /// </summary>
    /// <returns>An IQueryable of active Vendor entities.</returns>
    IQueryable<Vendor> ListAllQueryable();

    /// <summary>
    ///  Retrieves a Vendor entity by its ID, including related Contactos and location entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Vendor if founded, otherwise null</returns>
    Task<Vendor?> FindByIdAsync(byte id);

    /// <summary>
    /// Checks if a Vendor with the specified ID exists and is active.
    /// </summary>
    /// <param name="id">The unique identifier of the Vendor.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsVendorAsync(byte id);

    /// <summary>
    /// Creates a new Vendor entity in the database.
    /// </summary>
    /// <param name="vendor">The Vendor entity to be created.</param>
    /// <returns>Vendor</returns>
    Task<Vendor> CreateVendorAsync(Vendor vendor);

    /// <summary>
    /// Updates an existing Vendor entity in the database.
    /// </summary>
    /// <param name="vendor">The Vendor entity with updated information.</param>
    /// <returns>Vendor</returns>
    Task<Vendor> UpdateVendorAsync(Vendor vendor);

    /// <summary>
    /// Marks a Vendor entity as inactive instead of deleting it from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the Vendor to be marked as inactive.</param>
    /// <returns>The unique identifier of the Inventory to delete.</</returns>
    Task<bool> DeleteVendorAsync(byte id);
}