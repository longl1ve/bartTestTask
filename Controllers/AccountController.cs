using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountWithContact([FromBody] CreateAccountDto dto)
    {
        if (await _context.Accounts.AnyAsync(a => a.Name == dto.AccountName))
            return BadRequest("Account with this name already exists.");

        var existingContact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == dto.ContactEmail);

        if (existingContact != null)
            return BadRequest("A contact with this email already exists.");

        var account = new Account
        {
            Name = dto.AccountName,
            Contacts = new List<Contact>()
        };

        if (string.IsNullOrWhiteSpace(dto.ContactFirstName))
            return BadRequest("Contact first name is required");
        else if (string.IsNullOrWhiteSpace(dto.ContactLastName))
            return BadRequest("Contact last name is required");
        else if (string.IsNullOrWhiteSpace(dto.ContactEmail))
            return BadRequest("Contact email is required");
        else {
            var contact = new Contact
            {
                FirstName = dto.ContactFirstName,
                LastName = dto.ContactLastName,
                Email = dto.ContactEmail,
                Account = account
            };

            account.Contacts.Add(contact);
        }

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return Ok("Account with contact created successfully.");
    }
}