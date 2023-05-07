using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Infrastructure.EmailService.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 This code defines an extension method called AddInfrastructureServices that extends the IServiceCollection interface. 
It is used to register and configure various infrastructure services in the application's dependency injection container.

Here's what the code does:

- It receives an IServiceCollection and an IConfiguration as parameters.
The EmailSettings section from the configuration is bound to the EmailSettings class, which is registered as a configuration option.

- The IEmailSender interface is registered with the EmailSender class as its implementation. This means that when an IEmailSender 
dependency is requested, an instance of EmailSender will be provided.

- The IAppLogger<T> interface is registered as a generic type, where T can be any type. The LoggerAdapter<T> class 
is specified as the implementation. This allows for logging with different logger instances based on the type specified.

- The modified IServiceCollection is returned.

Overall, this code sets up the necessary infrastructure services in the application's dependency injection container. These services include email-related services like IEmailSender and EmailSettings, as well as logging services with IAppLogger<T> and LoggerAdapter<T>.
 */

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection
            services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}