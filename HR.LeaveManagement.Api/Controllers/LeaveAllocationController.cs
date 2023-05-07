using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/*
 //--- This code represents a controller class in an ASP.NET Core API for managing leave allocations. 
It handles various HTTP requests related to leave allocations, including getting a list of leave allocations, 
getting details of a specific leave allocation, creating a new leave allocation, updating an existing leave allocation, 
and deleting a leave allocation. The controller uses MediatR for handling the corresponding queries and commands, 
and it returns appropriate HTTP responses based on the operation results. ---//


 - The Get method handles a GET request to retrieve a list of leave allocations. 
It sends a GetLeaveAllocationListQuery request to the mediator to fetch the leave allocations 
and returns an Ok response with the list of leave allocations.

- [ProducesResponseType(201)] indicates that the method can return an HTTP response with a status code of 201 (Created) if the request is successful. This is commonly used for POST methods to indicate the successful creation of a new resource.

- [ProducesResponseType(400)] indicates that the method can return an HTTP response with a status code of 400 (Bad Request) if the request is invalid or missing required parameters. It typically signifies a client error.

- [ProducesResponseType(StatusCodes.Status404NotFound)] indicates that the method can return an HTTP response with a status code of 404 (Not Found) if the requested resource is not found. This is commonly used when fetching details of a specific resource using its identifier.

- The Get method with the {id} parameter handles a GET request to retrieve details of a
specific leave allocation identified by the provided id. It sends a GetLeaveAllocationDetailQuery 
request to the mediator with the specified id and returns an Ok response with the leave allocation details.

- The Post method handles a POST request to create a new leave allocation. It expects a CreateLeaveAllocationCommand object 
in the request body, which contains the necessary information for creating the leave allocation. It sends the command to the mediator, 
and upon successful creation, it returns a Created response with the location of the newly created resource.

- The Put method handles a PUT request to update an existing leave allocation. It expects an UpdateLeaveAllocationCommand 
object in the request body, which contains the updated information for the leave allocation identified by the provided id. 
It sends the command to the mediator, and if the update is successful, it returns a NoContent response indicating a successful update.

- The Delete method with the {id} parameter handles a DELETE request to remove a leave allocation identified by the provided id.
It creates a DeleteLeaveAllocationCommand object with the specified id, sends the command to the mediator, 
and upon successful deletion, it returns a NoContent response indicating a successful deletion.
 */



namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveAllocationsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }

    // GET api/<LeaveAllocationsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }

    // POST api/<LeaveAllocationsController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
