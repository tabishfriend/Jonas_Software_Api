﻿using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAll()
        {
            var res = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<bool> Add(EmployeeInfo employeeInfo)
        {
            Employee employee = new Employee();
            employee.SiteId = employeeInfo.SiteId;
            employee.CompanyCode = employeeInfo.CompanyCode;
            employee.EmployeeCode = employeeInfo.EmployeeCode;
            employee.EmployeeName = employeeInfo.EmployeeName;
            employee.Occupation = employeeInfo.Occupation;
            employee.EmployeeStatus = employeeInfo.EmployeeStatus;
            employee.EmailAddress = employeeInfo.EmailAddress;
            employee.Phone = employeeInfo.Phone;
            employee.LastModified = employeeInfo.LastModified;
            return await _employeeRepository.SaveEmployee(employee);
        }
    }
}
