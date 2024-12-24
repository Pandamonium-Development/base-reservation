using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryHoliday
{
    /// <summary>
    /// List all active Holidays.
    /// </summary>
    /// <returns>ICollection of Holiday</returns>
    Task<ICollection<Holiday>> ListAllAsync();

    /// <summary>
    /// Finds an active Holiday by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Holiday.</param>
    /// <returns>Holiday if founded, otherwise null</returns>
    Task<Holiday?> FindByIdAsync(byte id);

    /// <summary>
    /// Create a new Holyday
    /// </summary>
    /// <param name="holiday"> Holyday model to be add </param>
    /// <returns>Holiday</returns>
    Task<Holiday> CreateHolidayAsync(Holiday holiday);

    /// <summary>
    /// Updates an existing Holiday.
    /// </summary>
    /// <param name="holiday">The Holiday entity to update. </param>
    /// <returns>Holiday</returns>
    Task<Holiday> UpdateHolidayAsync(Holiday holiday);

    /// <summary>
    /// Checks if a Holiday with the specified identifier exists and is active.    
    /// /// </summary>
    /// <param name="id">The unique identifier of the Holiday to check for existence. </param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsHolidayAsync(byte id);

    /// <summary>
    /// Delete a Holiday 
    /// </summary>
    /// <param name="id"> The unique identifier of the Holiday model is removed</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    Task<bool> DeleteHolidayAsync(byte id);
}