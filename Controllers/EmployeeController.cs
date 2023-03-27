using CRUDWebAPI.Database;
using CRUDWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this._employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employee = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody]Employee emp)
        {
            emp.Id = new Guid();

            await _employeeDbContext.Employees.AddAsync(emp);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,[FromBody] Employee emp)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(a => a.Id == id);
            if(employee!=null)
            {
                employee.Name = emp.Name;
                employee.MobileNo = emp.MobileNo;
                employee.EmailId = emp.EmailId;
                await _employeeDbContext.SaveChangesAsync();
                return Ok(emp);
;            }
            else
            {
                return NotFound("Employee Not Found");
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(a => a.Id == id);
            if (employee != null)
            {
                _employeeDbContext.Employees.Remove(employee);
                await _employeeDbContext.SaveChangesAsync();
                return Ok(employee);
                ;
            }
            else
            {
                return NotFound("Employee Not Found");
            }
        }
    }
}
