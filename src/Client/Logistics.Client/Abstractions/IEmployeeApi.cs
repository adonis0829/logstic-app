﻿using Logistics.Client.Models;
using Logistics.Shared;
using Logistics.Shared.Models;

namespace Logistics.Client.Abstractions;

public interface IEmployeeApi
{
    Task<Result<EmployeeDto>> GetEmployeeAsync(string userId);
    Task<PagedResult<EmployeeDto>> GetEmployeesAsync(SearchableQuery query);
    Task<Result> CreateEmployeeAsync(CreateEmployee command);
    Task<Result> UpdateEmployeeAsync(UpdateEmployee command);
    Task<Result> DeleteEmployeeAsync(string userId);
}
