using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TrucksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrucksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{truckId}")]
    public async Task<ActionResult<Truck>> GetTruck(int truckId)
    {
        try
        {
            var truck = await _mediator.Send(new GetTruckQuery(truckId));
            return Ok(truck);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Truck>>> GetAllTrucks()
    {
        try
        {
            var trucks = await _mediator.Send(new GetAllTrucksQuery());
            return Ok(trucks);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("filtered")]
    public async Task<ActionResult<IEnumerable<Truck>>> GetFilteredTrucks(
        string? name = null,
        TruckStatus? status = null,
        string? description = null)
    {
        try
        {
            var trucks = await _mediator.Send(new GetFilteredTrucksQuery(name, status, description));
            return Ok(trucks);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    [HttpPost("insert")]
    public async Task<ActionResult<Truck>> InsertTruck([FromBody] InsertTruckCommand command)
    {
        try
        {
            var truck = await _mediator.Send(command);
            return Ok(truck);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost("updateStatus")]
    public async Task<ActionResult<Truck>> UpdateTruckStatus(int truckId, TruckStatus status)
    {
        try
        {
            var truck = await _mediator.Send(new UpdateTruckStatusCommand(truckId, status));
            return Ok(truck);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<Truck>> UpdateTruck([FromBody] UpdateTruckCommand command)
    {
        try
        {
            var truck = await _mediator.Send(command);
            return Ok(truck);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpDelete("{truckId}")]
    public async Task<ActionResult<IEnumerable<Truck>>> DeleteTruck(int truckId)
    {
        try
        {
            var trucks = await _mediator.Send(new DeleteTruckCommand(truckId));
            return Ok(trucks);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
