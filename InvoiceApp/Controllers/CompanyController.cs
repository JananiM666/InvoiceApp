using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    [ApiController]
    [Route("api/company")]
    [Authorize(Roles = "Admin")]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE / UPDATE
        [HttpPost]
        public IActionResult SaveCompany([FromBody] CompanyProfile model)
        {
            var existing = _context.CompanyProfiles.FirstOrDefault();

            if (existing == null)
            {
                _context.CompanyProfiles.Add(model);
            }
            else
            {
                existing.Name = model.Name;
                existing.Address = model.Address;
                existing.State = model.State;
                existing.GSTIN = model.GSTIN;
                existing.BankDetails = model.BankDetails;
            }

            _context.SaveChanges();

            return Ok("Company profile saved");
        }

        // GET
        [HttpGet]
        public IActionResult GetCompany()
        {
            var company = _context.CompanyProfiles.FirstOrDefault();
            return Ok(company);
        }
    }
}