using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/*
This code is an example of an entity type configuration in Entity Framework Core. 
It specifically configures the LeaveType entity for the database context.

- The LeaveTypeConfiguration class implements the IEntityTypeConfiguration<LeaveType> interface, 
which requires the Configure method to be implemented. Inside the Configure method, the behavior and 
properties of the LeaveType entity are defined.

- In this configuration, the LeaveType entity is mapped to a database table, and its properties are specified. 
It includes the configuration for the Id, Name, DefaultDays, DateCreated, and DateModified properties of the LeaveType entity.

- The HasData method is used to specify initial data for the LeaveType entity. In this case, it adds a single 
LeaveType object with predefined values for its properties.

- Additionally, the builder.Property method is used to configure the Name property of the LeaveType entity. 
It specifies that the Name property is required and has a maximum length of 100 characters.

Overall, this code sets up the configuration for the LeaveType entity in the database, including defining its properties, 
initial data, and any additional constraints or behaviors.
 */

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDays = 10,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            );

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}