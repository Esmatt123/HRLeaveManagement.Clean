using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocationForEmployee;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocationForEmployee;
using HR.LeaveManagement.Application.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocationForEmployee
{
    public class CreateLeaveAllocationForEmployeeCommandHandler : IRequestHandler<CreateLeaveAllocationForEmployeeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;

        public CreateLeaveAllocationForEmployeeCommandHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService)
        {
            _mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._userService = userService;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationForEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Get Leave type for allocations
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            // Get Employees
            var employee = await _userService.GetEmployee(request.UserId);

            //Get Period (using current year here)
            var period = DateTime.Now.Year;

            //Assign Allocations IF an allocation doesn't elready exist for the period and leave type
            var allocations = new List<Domain.LeaveAllocation>();

            foreach (var leaveType in leaveTypes)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(employee.Id, leaveType.Id, period);

                if (allocationExists == false)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }
            }

            if (allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);
            }

            return Unit.Value;
        }
    }
}