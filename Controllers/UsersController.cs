using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User(1, "alice@example.com", "Alice"),
        new User(2, "bob@example.com", "Bob")
    };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
        => Ok(Users);

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public ActionResult<User> Create(UserCreate request)
    {
        var nextId = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
        var user = new User(nextId, request.Email, request.Name);
        Users.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, UserUpdate request)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return NotFound();
        }

        user.Name = request.Name;
        user.Email = request.Email;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return NotFound();
        }

        Users.Remove(user);
        return NoContent();
    }
}
