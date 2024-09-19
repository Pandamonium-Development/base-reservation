using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified holiday service.
/// </summary>
/// <param name="serviceFeriado">The service used for holiday operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class FeriadoController(IServiceFeriado serviceFeriado) : ControllerBase
{
    /// <summary>
    /// Retrieves all holidays.
    /// </summary>
    /// <returns>A list of all holidays.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseFeriadoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var holidays = await serviceFeriado.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, holidays);
    }

    /// <summary>
    /// Retrieves a specific holiday by its ID.
    /// </summary>
    /// <param name="idFeriado">The ID of the holiday.</param>
    /// <returns>The details of the specified holiday.</returns>
    [HttpGet("{idFeriado}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseFeriadoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte idFeriado)
    {
        var holiday = await serviceFeriado.FindByIdAsync(idFeriado);
        return StatusCode(StatusCodes.Status200OK, holiday);
    }

    /// <summary>
    /// Creates a new holiday.
    /// </summary>
    /// <param name="holiday">The holiday data to be created.</param>
    /// <returns>The created holiday.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseFeriadoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateFeriadoAsync([FromBody] RequestFeriadoDto holiday)
    {
        ArgumentNullException.ThrowIfNull(holiday);
        var result = await serviceFeriado.CreateFeriadoAsync(holiday);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates an existing holiday by its ID.
    /// </summary>
    /// <param name="idFeriado">The ID of the holiday to update.</param>
    /// <param name="holiday">The updated holiday data.</param>
    /// <returns>The updated holiday.</returns>
    [HttpPut("{idFeriado}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseFeriadoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateFeriadoAsync(byte idFeriado, [FromBody] RequestFeriadoDto holiday)
    {
        ArgumentNullException.ThrowIfNull(holiday);
        var result = await serviceFeriado.UpdateFeriadoAsync(idFeriado, holiday);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a holiday by its ID.
    /// </summary>
    /// <param name="idFeriado">The ID of the holiday to delete.</param>
    /// <returns>The deleted holiday.</returns>
    [HttpDelete("{idFeriado}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseFeriadoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteFeriadoAsync(byte idFeriado)
    {
        var holiday = await serviceFeriado.DeleteFeriadoAsync(idFeriado);
        return StatusCode(StatusCodes.Status200OK, holiday);
    }
}