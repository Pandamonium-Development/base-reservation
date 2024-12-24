using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule block calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class BranchScheduleBlockController(IServiceBranchScheduleBlock serviceBloqueo) : ControllerBase
{
    /// <summary>
    /// Create new block of branch's schedule
    /// </summary>
    /// <param name="branchScheduleBlock">Block request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBranchScheduleBlockDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateBranchScheduleBlockAsync([FromBody] RequestBranchScheduleBlockDto branchScheduleBlock)
    {
        ArgumentNullException.ThrowIfNull(branchScheduleBlock);
        var result = await serviceBloqueo.CreateBranchScheduleBlockAsync(branchScheduleBlock);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Assign new blocks of branch's schedule
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <param name="branchScheduleBlocks">List Block request model to be assign</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/BranchSchedule/{branchScheduleId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBranchScheduleBlockDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateBranchScheduleBlockAsync(short branchScheduleId, [FromBody] IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks)
    {
        ArgumentNullException.ThrowIfNull(branchScheduleBlocks);
        var result = await serviceBloqueo.CreateBranchScheduleBlockAsync(branchScheduleId, branchScheduleBlocks);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing block of branch's schedule
    /// </summary>
    /// <param name="idBranchScheduleBlock">Block branch's schedule id</param>
    /// <param name="branchScheduleBlock">Block request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idBranchScheduleBlock}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleBlockDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateBranchScheduleBlockAsync(long idBranchScheduleBlock, [FromBody] RequestBranchScheduleBlockDto branchScheduleBlock)
    {
        ArgumentNullException.ThrowIfNull(branchScheduleBlock);
        var result = await serviceBloqueo.UpdateBranchScheduleBlockAsync(idBranchScheduleBlock, branchScheduleBlock);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}