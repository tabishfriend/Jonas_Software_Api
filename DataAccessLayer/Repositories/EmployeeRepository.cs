using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var itemRepo = (await _employeeDbWrapper.FindAsync(t =>
                 t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode)))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeCode = employee.EmployeeCode;
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = DateTime.UtcNow;
                return await _employeeDbWrapper.UpdateAsync(itemRepo);
            }
            else
            {
                return await _employeeDbWrapper.InsertAsync(employee);
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }
    }
}
