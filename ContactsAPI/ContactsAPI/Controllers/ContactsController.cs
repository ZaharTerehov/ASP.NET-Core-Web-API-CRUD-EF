using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ContactsController : Controller
	{
		private readonly ContactsAPIDbContext dbContext;

		public ContactsController(ContactsAPIDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetContacts()
		{
			return Ok(await dbContext.Contacts.ToListAsync());
		}

		[HttpPost]
		public async Task<IActionResult> AddContact(AddContactRequest addContactrRequest)
		{
			var contact = new Contact()
			{
				Id = Guid.NewGuid(),
				Address = addContactrRequest.Address,
				Email = addContactrRequest.Email,
				FullName = addContactrRequest.FullName,
				Phone = addContactrRequest.Phone
			};

			await dbContext.Contacts.AddAsync(contact);
			await dbContext.SaveChangesAsync();

			return Ok(contact);
		}
	}
}
