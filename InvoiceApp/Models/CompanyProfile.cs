namespace InvoiceApp.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? State { get; set; }

        public string? GSTIN { get; set; }

        public string? BankDetails { get; set; }
    }
}