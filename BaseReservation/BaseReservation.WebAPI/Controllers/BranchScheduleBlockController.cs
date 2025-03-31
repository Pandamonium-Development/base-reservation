using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule block calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class BranchScheduleBlockController(IServiceBranchScheduleBlock serviceBlock) : ControllerBase
{
    /// <summary>
    /// Get block with specific id
    /// </summary>
    /// <param name="branchScheduleßlockId">Block id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchScheduleßlockId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleBlockDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long branchScheduleßlockId)
    {
        var block = await serviceBlock.FindByIdAsync(branchScheduleßlockId);
        return StatusCode(StatusCodes.Status200OK, block);
    }

    /// <summary>
    /// Get blocks by schedule
    /// </summary>
    /// <param name="scheduleId">Schedule id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Schedule/{scheduleId}/Block")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseBranchScheduleBlockDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByBranchAsync(byte scheduleId)
    {
        var schedules = await serviceBlock.ListAllByBranchScheduleAsync(scheduleId);
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

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
        var result = await serviceBlock.CreateBranchScheduleBlockAsync(branchScheduleBlock);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Assign new blocks of branch's schedule
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <param name="branchScheduleBlocks">List Block request model to be assign</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/BranchSchedule/{branchScheduleId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateBranchScheduleBlockAsync(short branchScheduleId, [FromBody] IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks)
    {
        ArgumentNullException.ThrowIfNull(branchScheduleBlocks);
        var result = await serviceBlock.CreateBranchScheduleBlockAsync(branchScheduleId, branchScheduleBlocks);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing block of branch's schedule
    /// </summary>
    /// <param name="blockId">Block branch's schedule id</param>
    /// <param name="branchScheduleBlock">Block request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{blockId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchScheduleBlockDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateBranchScheduleBlockAsync(long blockId, [FromBody] RequestBranchScheduleBlockDto branchScheduleBlock)
    {
        ArgumentNullException.ThrowIfNull(branchScheduleBlock);
        var result = await serviceBlock.UpdateBranchScheduleBlockAsync(blockId, branchScheduleBlock);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a block by its ID.
    /// </summary>
    /// <param name="blockId">The ID of the block to delete.</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{blockId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteBranchScheduleBlockAsync(long blockId)
    {
        var branch = await serviceBlock.DeleteBranchScheduleBlockAsync(blockId);
        return StatusCode(StatusCodes.Status200OK, branch);
    }
}