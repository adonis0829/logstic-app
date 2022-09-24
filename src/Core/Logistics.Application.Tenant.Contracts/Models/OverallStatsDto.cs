﻿namespace Logistics.Application.Contracts.Models;

public class OverallStatsDto
{
    public string? OwnerName { get; set; }
    public int EmployeesCount { get; set; }
    public int ManagersCount { get; set; }
    public int DispatchersCount { get; set; }
    public int DriversCount { get; set; }
    public double IncomeAllTime { get; set; }
    public double DistanceAllTime { get; set; }
}