using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.App_Start;
using Serilog;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: api/Employee
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                Log.Information("Request: Get, Controller :Employee");
                var items =  await _employeeService.GetAll();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                Log.Error("Internal server error",ex);
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] EmployeeDto value)
        {
            try
            {
                Log.Information("Request: Post, Controller :Employee");

                EmployeeInfo employee = new EmployeeInfo();
                employee.SiteId = value.SiteId;
                employee.CompanyCode = value.CompanyCode;
                employee.EmployeeCode = value.EmployeeCode;
                employee.EmployeeName = value.EmployeeName;
                employee.Occupation = value.OccupationName;
                employee.EmployeeStatus = value.EmployeeStatus;
                employee.EmailAddress = value.EmailAddress;
                employee.Phone = value.PhoneNumber;
                DateTime.TryParse(value.LastModifiedDateTime, out var temp);
                employee.LastModified = temp;
                return await _employeeService.Add(employee);
            }
            catch (Exception ex)
            {
                Log.Error("Internal server error", ex);
                return false;
            }
        }
    }
}