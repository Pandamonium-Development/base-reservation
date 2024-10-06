using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of unit of measure calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class UnitMeasureController(IServiceUnitMeasure serviceUnitMeasure) : ControllerBase
{
    /// <summary>
    /// Get list of all unit of measures
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUnitMeasureDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var unitsMeasure = await serviceUnitMeasure.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, unitsMeasure);
    }
}