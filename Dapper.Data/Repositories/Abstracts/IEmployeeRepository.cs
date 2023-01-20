using Dapper.Data.Models.Employee;
using Dapper.Models.Models.Employees;

namespace Dapper.Data.Repositories.Abstracts
{
    public interface IEmployeeRepository
    {
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<int> AddEmployeeReturnIdAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(string emailOrName);
        Task<bool> UpdateEmployeeAsync(Employee employee);
    }
}
