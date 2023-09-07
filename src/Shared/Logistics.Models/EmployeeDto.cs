﻿namespace Logistics.Models;

public class EmployeeDto
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; }
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    public TenantRoleDto[]? Roles { get; set; }
}
