using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/items")]
    [Authorize(Roles = "Admin,Accountant")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Item model)
        {
            _context.Items.Add(model);
            _context.SaveChanges();

            return Ok("Item added");
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_context.Items.ToList());
        }
    }
}