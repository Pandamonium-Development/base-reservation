using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class BranchScheduleController(IServiceBranchSchedule serviceBranchSchedule) : ControllerBase
{
    /// <summary>
    /// Get branch schedule with specific id
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchScheduleId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(short branchScheduleId)
    {
        var schedule = await serviceBranchSchedule.FindByIdAsync(branchScheduleId);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Get schedules by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{branchId}/Horario")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByBranchAsync(byte branchId)
    {
        var schedules = await serviceBranchSchedule.ListAllByBranchAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

    /// <summary>
    /// Assign schedules to a branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branchSchedule">List of schedules</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Branch/{branchId}/Horario")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateBranchScheduleAsync(byte branchId, [FromBody] IEnumerable<RequestBranchScheduleDto> branchSchedule)
    {
        ArgumentNullException.ThrowIfNull(branchSchedule);
        var result = await serviceBranchSchedule.CreateBranchScheduleAsync(branchId, branchSchedule);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}