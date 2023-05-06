using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocationForEmployee
{
    public class CreateLeaveAllocationForEmployeeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
