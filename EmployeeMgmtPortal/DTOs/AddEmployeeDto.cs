using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmtPortal.DTOs;

public class AddEmployeeDto
{
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal Salary { get; set; }
}
