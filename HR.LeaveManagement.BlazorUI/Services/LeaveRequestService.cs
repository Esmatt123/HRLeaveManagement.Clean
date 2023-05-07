using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Services.Base;

/*
 This code defines the LeaveRequestService class, which is a service responsible for handling leave request-related operations in a Blazor UI application. Here's an overview of what this code does:

- The class LeaveRequestService is declared and implements the ILeaveRequestService interface.

- It inherits from the BaseHttpService class, which likely provides common functionality for making HTTP requests.

- The IMapper and ILocalStorageService dependencies are injected into the constructor via dependency injection.

- The class contains several methods that interact with the server-side API to perform various operations related to leave requests:

- ApproveLeaveRequest: Sends a request to update the approval status of a leave request.

- CancelLeaveRequest: Sends a request to cancel a leave request.

- CreateLeaveRequest: Sends a request to create a new leave request.

- DeleteLeaveRequest: Throws a NotImplementedException since it is not implemented.

- GetAdminLeaveRequestList: Retrieves a list of leave requests for the admin view and returns a view model.

- GetLeaveRequest: Retrieves a specific leave request by its ID and returns a view model.

- GetUserLeaveRequests: Retrieves leave requests and allocations for the logged-in user and returns a view model.

- The methods use the injected dependencies, such as IMapper, to map the server-side response objects to the corresponding view models.

- The methods handle exceptions and convert them into appropriate response objects.

- The service interacts with the server-side API by making asynchronous HTTP requests using the injected IClient instance.


Overall, the LeaveRequestService encapsulates the logic for handling leave request-related operations, including creating, 
updating, canceling, and retrieving leave requests, and provides data in the form of view models to be consumed by the Blazor 
UI components.
 */

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IMapper _mapper;

        public LeaveRequestService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
        {
            try
            {
                var response = new Response<Guid>();
                var request = new ChangeLeaveRequestApprovalCommand { Approved = approved, Id = id };
                await _client.UpdateApprovalAsync(request);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Guid>> CancelLeaveRequest(int id)
        {
            try
            {
                var response = new Response<Guid>();
                var request = new CancelLeaveRequestCommand { Id = id };
                await _client.CancelRequestAsync(request);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            try
            {
                var response = new Response<Guid>();
                CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

                await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public Task<Response<Guid>> DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: false);

            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };
            return model;
        }

        public async Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            await AddBearerToken();
            var leaveRequest = await _client.LeaveRequestsGETAsync(id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);
        }

        public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
        {
            await AddBearerToken();
            var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: true);
            var allocations = await _client.LeaveAllocationsAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            return model;
        }
    }
}
