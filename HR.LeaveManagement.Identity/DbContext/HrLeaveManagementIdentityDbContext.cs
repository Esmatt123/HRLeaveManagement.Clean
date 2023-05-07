using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

/*
//--- This code defines a custom DbContext class called HrLeaveManagementIdentityDbContext which is used for 
managing the identity-related data in the HR Leave Management system. It extends the IdentityDbContext<ApplicationUser> 
class provided by Microsoft.AspNetCore.Identity.EntityFrameworkCore, which is a pre-defined DbContext class for managing 
user authentication and authorization data. The constructor accepts the DbContextOptions<HrLeaveManagementIdentityDbContext> 
options for configuring the DbContext. The OnModelCreating method is overridden to apply any entity configurations defined 
in the assembly where the HrLeaveManagementIdentityDbContext class is located. ---//


 */

namespace HR.LeaveManagement.Identity.DbContext
{
    public class HrLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public HrLeaveManagementIdentityDbContext(DbContextOptions<HrLeaveManagementIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementIdentityDbContext).Assembly);
        }
    }
}
