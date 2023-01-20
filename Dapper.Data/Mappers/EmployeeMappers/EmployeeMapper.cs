using Dapper.Data.Models.Employee;
using Dapper.FluentMap.Mapping;
using Dapper.Models.Models.Employees;

namespace Dapper.Data.Mappers.EmployeeMappers
{
    public class EmployeeMapper : EntityMap<Employee>
    {
        public EmployeeMapper()
        {
            Map(p => p.Id)
                      .ToColumn("id");
            Map(p => p.Name)
                .ToColumn("name");
            Map(p => p.Email)
               .ToColumn("email");
            Map(p => p.DateOfBirth)
              .ToColumn("dob");
            Map(p => p.Gender)
             .ToColumn("gender");
        }
    }
}
