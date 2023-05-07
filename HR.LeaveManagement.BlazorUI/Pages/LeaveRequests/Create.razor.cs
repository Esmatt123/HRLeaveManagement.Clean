using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

/*
//--- This code represents a Blazor component for creating a leave request. Let's break down its functionality:
Overall, this component handles the creation of a leave request by retrieving available leave types, 
capturing user input, and invoking the appropriate service methods to create the leave request. ---//


The component is defined with the name "Create" and marked as a partial class.
The component imports necessary dependencies and services using the using statements.
The component declares properties for injecting instances of services using the [Inject] attribute.
The NavigationManager is injected to provide navigation functionality.
The component defines a property LeaveRequest of type LeaveRequestVM, which represents the leave request being created.
The component defines a property leaveTypeVMs of type List<LeaveTypeVM>, which holds the collection of available leave types.
The OnInitializedAsync lifecycle method is overridden and executed when the component is initialized. 
It calls the GetLeaveTypes method of the leaveTypeService to populate the leaveTypeVMs collection with the available leave types.
The HandleValidSubmit method is invoked when the form is submitted and the form data is valid. 
It calls the CreateLeaveRequest method of the leaveRequestService to create the leave request based on the LeaveRequest object. 
After successfully creating the leave request, it navigates to the "/leaverequests/" page using the NavigationManager.
 */

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] 
        ILeaveTypeService leaveTypeService { get; set; }
        [Inject]
        ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();
        List<LeaveTypeVM> leaveTypeVMs { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            leaveTypeVMs = await leaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            await leaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}
