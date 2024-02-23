namespace CrudAppDotNet8.Models.Users
{
    public record UserDto
        (
            int Id,
            string FirstName,
            string LastName,
            string Email,
            string Role
        );
}
