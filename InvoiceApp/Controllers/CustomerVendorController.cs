using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/party")]
    [Authorize(Roles = "Admin,Accountant")]
    public class CustomerVendorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerVendorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerVendor model)
        {
            _context.CustomerVendors.Add(model);
            _context.SaveChanges();

            return Ok("Saved");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CustomerVendors.ToList());
        }
    }
}