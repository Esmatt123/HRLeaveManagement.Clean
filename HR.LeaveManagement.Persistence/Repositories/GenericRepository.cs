using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
//--- This code defines a generic repository class called GenericRepository<T> that implements the IGenericRepository<T> interface. 
It provides basic CRUD operations for entities of type T. The constructor accepts an instance of the HrDatabaseContext class, 
which represents the database context for HR Leave Management. The class includes methods such as CreateAsync, DeleteAsync,
GetAsync, GetByIdAsync, and UpdateAsync to perform respective database operations using the provided DbContext.
These methods manipulate the entities in the context and persist the changes to the database using the SaveChangesAsync method. ---//

- The T entity parameter in the method signatures represents an instance of the entity type T. 
It is used as a parameter for methods such as CreateAsync, DeleteAsync, and UpdateAsync to perform the 
respective operations on the provided entity.

- The repository pattern is a software design pattern that provides an abstraction layer between the data access logic and the business logic of an application. It is commonly used to manage data persistence and retrieval operations in an organized and maintainable manner.
In the repository pattern, a repository acts as a mediator between the application and the data source (usually a database). 
It encapsulates the logic for querying, creating, updating, and deleting entities, providing a consistent interface for data access.

The key benefits of using the repository pattern include:

Separation of concerns: The repository separates the data access logic from the business logic, 
promoting a modular and maintainable codebase.
Abstraction: The repository provides an abstraction layer that shields the application from the underlying 
data storage implementation details.
Testability: By abstracting the data access logic, it becomes easier to write unit tests for the application's 
business logic without relying on a physical database.
Centralized data access logic: The repository consolidates all data access operations within a single component, 
promoting code reuse and consistency.
Typically, a repository interface defines a set of methods for performing CRUD (Create, Read, Update, Delete) 
operations on entities. Concrete implementations of the repository interface interact with the underlying data source, 
such as a database, using technologies like Entity Framework, ADO.NET, or other ORM (Object-Relational Mapping) frameworks.

The repository pattern helps in achieving a more maintainable and scalable architecture by promoting separation of concerns 
and providing a clear and consistent way to interact with the data layer of an application.
 */

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HrDatabaseContext _context;

        public GenericRepository(HrDatabaseContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}