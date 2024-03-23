namespace BeyondComputersNi.Dal.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? Name { get; set; }

    public List<Computer>? Computers { get; set; }
}
