using Dapper.Models.Models.Employees;
using Dapper.Services.Abstracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet, Route("employee-filter/{emailOrName}")]
        public async Task<IActionResult> Get([FromRoute] string emailOrName)
        {
            try
            {
                if (string.IsNullOrEmpty(emailOrName))
                    throw new ArgumentNullException(nameof(emailOrName));
                return Ok(await _employeeService.GetFilteredEmployeesAsync(emailOrName));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ProducesResponseType(typeof(EmployeeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            try
            {
                if (!id.HasValue)
                    throw new ArgumentNullException(nameof(id));

                if (id.Value <= 0)
                    return BadRequest("id must be greater than 0");

                var response = await _employeeService.GetEmployeeByIdAsync(id.Value);

                if (response == null)
                    return NotFound($"Employee not exist!");

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddorEditEmployeeModel employeeModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data!");

                var response = await _employeeService.AddEmployeeAsync(employeeModel);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost("add-employee-return-id")]
        public async Task<IActionResult> AddEmployeeReturnId([FromBody] AddorEditEmployeeModel employeeModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data!");

                var response = await _employeeService.AddEmployeeAsync(employeeModel);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int? id, [FromBody] AddorEditEmployeeModel employeeModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data!");

                if (!id.HasValue)
                    throw new ArgumentNullException(nameof(id));

                if (id.Value <= 0)
                    return BadRequest("id must be greater than 0");

                if (id.Value != employeeModel.Id)
                    return BadRequest("Invalid id");

                var employeeExist = await _employeeService.GetEmployeeByIdAsync(id.Value);

                if (employeeExist == null)
                    return NotFound($"Employee not exist!");

                var response = await _employeeService.UpdateEmployeeAsync(employeeModel);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            try
            {
                if (!id.HasValue)
                    throw new ArgumentNullException(nameof(id));

                if (id.Value <= 0)
                    return BadRequest("id must be greater than 0");

                var employeeExist = await _employeeService.GetEmployeeByIdAsync(id.Value);

                if (employeeExist == null)
                    return NotFound($"Employee not exist!");

                var response = await _employeeService.DeleteEmployeeAsync(id.Value);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
