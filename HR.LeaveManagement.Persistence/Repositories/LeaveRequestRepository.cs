using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

/*
 This code defines a LeaveRequestRepository class that extends a generic repository. 
It implements specific methods for retrieving leave requests with additional details from a database
using Entity Framework Core. These methods allow fetching leave requests with leave type details based 
on different criteria, such as all leave requests, leave requests for a specific user, or a specific leave request by ID.

- GetLeaveRequestsWithDetails: Retrieves all leave requests with their associated leave type details from the database. 
It filters out leave requests where the requesting employee ID is not empty or null.

- GetLeaveRequestsWithDetails(string userId): Retrieves leave requests with their associated leave type details for a 
specific user identified by their user ID.

- GetLeaveRequestsWithDetails(int id): Retrieves a specific leave request with its associated leave type details 
based on the provided leave request ID.
 */

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => !string.IsNullOrEmpty(q.RequestingEmployeeId))
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestsWithDetails(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveRequest;
        }
    }
}