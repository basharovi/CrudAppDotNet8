using CrudAppDotNet8.Entities;
using System.ComponentModel.DataAnnotations;

namespace CrudAppDotNet8.Models.Users;

public class CreateRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [EnumDataType(typeof(Role))]
    public required string Role { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [MinLength(6)]
    public required string Password { get; set; }

    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}
