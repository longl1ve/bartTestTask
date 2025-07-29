using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly AppDbContext _context;
    public IncidentController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentRequest request)
    {
        var account = await _context.Accounts
            .Include(a => a.Contacts)
            .FirstOrDefaultAsync(a => a.Name == request.AccountName);

        if (account == null)
            return NotFound("Account not found");

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

        if (contact != null)
        {
            contact.FirstName = request.ContactFirstName;
            contact.LastName = request.ContactLastName;

            if (contact.AccountId != account.Id)
            {
                contact.AccountId = account.Id;
            }
        }
        else
        {
            contact = new Contact
            {
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail,
                Account = account
            };
            _context.Contacts.Add(contact);
        }

        var incident = new Incident
        {
            Description = request.IncidentDescription,
            Account = account,
            Contact = contact
        };
        _context.Incidents.Add(incident);

        await _context.SaveChangesAsync();

        return Ok(new { incident.Name });
    }
}
