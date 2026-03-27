using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/tax")]
    [Authorize(Roles = "Admin")]
    public class TaxController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaxController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddTax([FromBody] TaxRate model)
        {
            _context.TaxRates.Add(model);
            _context.SaveChanges();

            return Ok("Tax added");
        }

        [HttpGet]
        public IActionResult GetTaxes()
        {
            return Ok(_context.TaxRates.ToList());
        }
    }
}