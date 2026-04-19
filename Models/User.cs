using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    public User() { }

    public User(int id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }

    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class UserCreate
{
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class UserUpdate
{
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
}
