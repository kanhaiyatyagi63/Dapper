
using Dapper.Data.Models.Employee;
using Dapper.Data.Repositories.Abstracts;
using Dapper.Models.Models.Employees;
using Dapper.Services.Abstracts;
using Microsoft.Data.SqlClient;

namespace Dapper.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeModel>> GetFilteredEmployeesAsync(string emailOrName)
        {
            try
            {
                var response = await _employeeRepository.GetFilteredEmployeesAsync(emailOrName);
                return response.Select(x => new EmployeeModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    Gender = x.Gender
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeModel?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var response = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (response == null)
                    return null;
                return new EmployeeModel()
                {
                    Name = response.Name,
                    Id = response.Id,
                    DateOfBirth = response.DateOfBirth,
                    Email = response.Email,
                    Gender = response.Gender
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddEmployeeAsync(AddorEditEmployeeModel addorEditEmployee)
        {
            try
            {
                var employee = new Employee()
                {
                    DateOfBirth = addorEditEmployee.DateOfBirth,
                    Email = addorEditEmployee.Email,
                    Gender = addorEditEmployee.Gender,
                    Id = addorEditEmployee.Id,
                    Name = addorEditEmployee.Name
                };
                return await _employeeRepository.AddEmployeeAsync(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addorEditEmployee"></param>
        /// <returns>Newly created employee id</returns>
        public async Task<int> AddEmployeeReturnIdAsync(AddorEditEmployeeModel addorEditEmployee)
        {
            try
            {
                var employee = new Employee()
                {
                    DateOfBirth = addorEditEmployee.DateOfBirth,
                    Email = addorEditEmployee.Email,
                    Gender = addorEditEmployee.Gender,
                    Id = addorEditEmployee.Id,
                    Name = addorEditEmployee.Name
                };
                return await _employeeRepository.AddEmployeeReturnIdAsync(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(AddorEditEmployeeModel addorEditEmployee)
        {
            try
            {
                var employee = new Employee()
                {
                    DateOfBirth = addorEditEmployee.DateOfBirth,
                    Email = addorEditEmployee.Email,
                    Gender = addorEditEmployee.Gender,
                    Id = addorEditEmployee.Id,
                    Name = addorEditEmployee.Name
                };
                return await _employeeRepository.UpdateEmployeeAsync(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                return await _employeeRepository.DeleteEmployeeAsync(id);
            }
            catch (Exception) { throw; }
        }
    }
}
