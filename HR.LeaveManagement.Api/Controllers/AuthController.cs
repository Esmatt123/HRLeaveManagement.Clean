using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocationForEmployee;
using HR.LeaveManagement.Application.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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