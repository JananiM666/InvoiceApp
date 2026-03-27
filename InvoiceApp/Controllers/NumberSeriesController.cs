using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/series")]
    [Authorize(Roles = "Admin")]
    public class NumberSeriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NumberSeriesController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE SERIES
        [HttpPost]
        public IActionResult CreateSeries([FromBody] NumberSeries model)
        {
            _context.NumberSeries.Add(model);
            _context.SaveChanges();

            return Ok("Series created");
        }

        // GET NEXT NUMBER (IMPORTANT)
        [HttpGet("next")]
        public IActionResult GetNextNumber()
        {
            var series = _context.NumberSeries.FirstOrDefault();

            if (series == null)
                return BadRequest("No series found");

            series.CurrentNumber += 1;
            _context.SaveChanges();

            var invoiceNumber = $"{series.Prefix}{series.CurrentNumber}";

            return Ok(invoiceNumber);
        }
    }
}