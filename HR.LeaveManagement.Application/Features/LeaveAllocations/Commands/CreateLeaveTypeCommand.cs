using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands
{
    public class CreateLeaveTypeCommand : IRequest<Unit>
    {
        public int LeaveTypeId { get; set; }
    }
}
