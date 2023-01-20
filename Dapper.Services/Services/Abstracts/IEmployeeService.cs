using Dapper.Models.Models.Employees;

namespace Dapper.Services.Abstracts
{
    public interface IEmployeeService
    {
        Task<bool> AddEmployeeAsync(AddorEditEmployeeModel addorEditEmployee);
        Task<int> AddEmployeeReturnIdAsync(AddorEditEmployeeModel addorEditEmployee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<EmployeeModel?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeModel>> GetFilteredEmployeesAsync(string emailOrName);
        Task<bool> UpdateEmployeeAsync(AddorEditEmployeeModel addorEditEmployee);
    }
}
