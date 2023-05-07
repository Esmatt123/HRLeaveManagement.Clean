using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocationForEmployee;
using HR.LeaveManagement.Application.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;


/*
 //--- The AuthController is an API controller with two endpoints for user authentication and registration. 
The Login endpoint receives a user's login credentials, authenticates them using the _authenticationService, 
and returns an AuthResponse. The Register endpoint receives user registration details, 
registers the user using the _authenticationService, and creates a leave allocation 
for the user before returning a RegistrationResponse. ---//

- `[Route("api/[controller]")]` specifies the route template for the controller, 
indicating that routes will start with "api/" followed by the controller name.

- `[ApiController]` applies conventions for API controllers,
simplifying the process of building APIs by providing default behavior for common scenarios.

- `Ok` is a method in ASP.NET Core that returns a successful HTTP 200 OK response with the specified data. 
It is commonly used in API controllers to send a response to the client.

- `HttpPost` is an attribute in ASP.NET Core that is used to decorate a controller action 
method to indicate that it should handle HTTP POST requests. 
It is part of the HTTP verb attributes that help define the supported HTTP methods for a given action.

- `ControllerBase` is a base class in ASP.NET Core that provides common functionality for controller classes. 
It contains methods and properties that enable handling of HTTP requests and generating HTTP responses. 
Controller classes derive from ControllerBase to implement specific actions and behaviors for handling 
client requests in an MVC (Model-View-Controller) pattern.
 */

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly IMediator mediator;

        public AuthController(IAuthService authenticationService, IMediator mediator)
        {
            this._authenticationService = authenticationService;
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            Console.WriteLine("Login method called with request: " + request);
            var response = await _authenticationService.Login(request);
            Console.WriteLine("Login method returned response: " + response);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var userRegistrationResponse = await _authenticationService.Register(request);
            await mediator.Send(new CreateLeaveAllocationForEmployeeCommand { UserId = userRegistrationResponse.UserId });
            return Ok(await _authenticationService.Register(request));
        }
    }
}