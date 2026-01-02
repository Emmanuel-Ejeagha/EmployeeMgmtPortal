using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmtPortal.Models.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public decimal Salary { get; set; }
}
