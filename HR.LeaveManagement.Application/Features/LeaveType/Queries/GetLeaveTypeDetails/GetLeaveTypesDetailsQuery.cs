using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{

    /*
     - record: In C#, a record is a reference type that represents an immutable set of values or data.
     */
    public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDto>
    {
    }
}