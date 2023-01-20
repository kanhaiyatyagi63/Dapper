
using Dapper.Data.Extensions;
using Dapper.Data.Models.Employee;
using Dapper.Data.Repositories.Abstracts;
using Dapper.Models.Models.Employees;
using Microsoft.Data.SqlClient;
using System;

namespace Dapper.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _sqlConnection;

        public EmployeeRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(string emailOrName)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@value", emailOrName);
                var response = await _sqlConnection.ExecuteProcedureAsync<Employee>("Sp_FilterEmployee", dynamicParameters);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var sqlCommand = "SELECT * FROM Employees WHERE id = @id";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                var response = await _sqlConnection.QueryFirstOrDefaultAsync<Employee>(sqlCommand, dynamicParameters);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method add the employee into database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>true for successfully inserted into db otherwise false</returns>
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            try
            {
                var sqlCommand = "INSERT into Employees (name, dob, email, gender) VALUES (@name, @dob, @email, @gender); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@name", employee.Name);
                dynamicParameters.Add("@dob", employee.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                dynamicParameters.Add("@email", employee.Email);
                dynamicParameters.Add("@gender", employee.Gender);

                var response = await _sqlConnection.QuerySingleAsync<int>(sqlCommand, dynamicParameters);
                if (response == 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This is another method to add employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Newly created employee Id</returns>
        public async Task<int> AddEmployeeReturnIdAsync(Employee employee)
        {
            try
            {
                var sqlCommand = "INSERT into Employees (name, dob, email, gender) VALUES (@name, @dob, @email, @gender); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@name", employee.Name);
                dynamicParameters.Add("@dob", employee.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                dynamicParameters.Add("@email", employee.Email);
                dynamicParameters.Add("@gender", employee.Gender);

                var response = await _sqlConnection.QuerySingleAsync<int>(sqlCommand, dynamicParameters);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var sqlCommand = "UPDATE Employees SET name = @name, dob = @dob, email = @email, gender = @gender WHERE id = @id;";
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@name", employee.Name);
                dynamicParameters.Add("@dob", employee.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                dynamicParameters.Add("@email", employee.Email);
                dynamicParameters.Add("@gender", employee.Gender);
                dynamicParameters.Add("@id", employee.Id);

                var response = await _sqlConnection.ExecuteAsync(sqlCommand, dynamicParameters);
                if (response == 0)
                    return false;

                return true;
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
                var sqlCommand = "Delete from Employees WHERE id = @id;";

                // another approach without dynamic parameter, you can pass annonymous object into it.
                var response = await _sqlConnection.ExecuteAsync(sqlCommand, new { id });
                if (response == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
