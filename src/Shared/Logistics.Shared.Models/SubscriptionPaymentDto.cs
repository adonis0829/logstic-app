﻿namespace Logistics.Shared.Models;

public class PayrollPaymentDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public PaymentDto? Payment { get; set; }
    public EmployeeDto? Employee { get; set; }
}
