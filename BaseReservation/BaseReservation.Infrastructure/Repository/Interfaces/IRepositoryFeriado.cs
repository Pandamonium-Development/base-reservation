using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryFeriado
{
    /// <summary>
    /// List all active Holidays.
    /// </summary>
    /// <returns>ICollection of Feriado</returns>
    Task<ICollection<Feriado>> ListAllAsync();

    /// <summary>
    /// Finds an active Holiday by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Holiday.</param>
    /// <returns>Feriado if founded, otherwise null</returns>
    Task<Feriado?> FindByIdAsync(byte id);

    /// <summary>
    /// Create a new Holyday
    /// </summary>
    /// <param name="feriado"> Holyday model to be add </param>
    /// <returns>Feriado</returns>
    Task<Feriado> CreateFeriadoAsync(Feriado feriado);

    /// <summary>
    /// Updates an existing Holiday.
    /// </summary>
    /// <param name="feriado">The Holiday entity to update. </param>
    /// <returns>Feriado</returns>
    Task<Feriado> UpdateFeriadoAsync(Feriado feriado);

    /// <summary>
    /// Checks if a Holiday with the specified identifier exists and is active.    
    /// /// </summary>
    /// <param name="id">The unique identifier of the Holiday to check for existence. </param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsFeriadoAsync(byte id);

    /// <summary>
    /// Delete a Holiday 
    /// </summary>
    /// <param name="id"> The unique identifier of the Holiday model is removed</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    Task<bool> DeleteFeriadoAsync(byte id);
}