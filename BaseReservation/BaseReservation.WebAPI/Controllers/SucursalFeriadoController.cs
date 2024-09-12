using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's holiday calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class SucursalFeriadoController(IServiceSucursalFeriado serviceSucursalFeriado) : ControllerBase
{
    /// <summary>
    /// Get list of all branch's holidays
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="anno">Year</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Sucursal/{idSucursal}/Feriado")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSucursalFeriadoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllFeriadosSucursaleAsync(byte idSucursal, [FromQuery] short? anno)
    {
        short? yearSeach = null;
        if (anno != null) yearSeach = anno == 0 ? (short)DateTime.Now.Year : anno.Value;
        var branchHolidays = await serviceSucursalFeriado.ListAllBySucursalAsync(idSucursal, yearSeach);
        return StatusCode(StatusCodes.Status200OK, branchHolidays);
    }

    /// <summary>
    /// Get branch holiday
    /// </summary>
    /// <param name="idSucursalFeriado">Branch holiday Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idSucursalFeriado}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalFeriadoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetFeriadoSucursalByIdAsync(short idSucursalFeriado)
    {
        var branchHolidays = await serviceSucursalFeriado.FindByIdAsync(idSucursalFeriado);
        return StatusCode(StatusCodes.Status200OK, branchHolidays);
    }

    /// <summary>
    /// Assign holidays to a branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursalFeriados">List of holidays</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Sucursal/{idSucursal}/Feriado")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateFeriadosSucursalAsync(byte idSucursal, [FromBody] IEnumerable<RequestSucursalFeriadoDto> sucursalFeriados)
    {
        ArgumentNullException.ThrowIfNull(sucursalFeriados);
        var result = await serviceSucursalFeriado.CreateSucursalFeriadosAsync(idSucursal, sucursalFeriados);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}