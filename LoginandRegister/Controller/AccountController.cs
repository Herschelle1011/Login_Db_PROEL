using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LoginandRegister.Data; // replace with your DbContext namespace
using LoginandRegister.Model;

[Route("account")]
public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;

    public AccountController(ApplicationDbContext db)
    {
        _db = db;
    }

    // POST: /account/register
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] string email, [FromForm] string password)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
        {
            return BadRequest("User already exists"); //already existed users
        }

        var user = new User
        {
            Email = email,
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Ok("User registered"); //saved registered
    }
}
