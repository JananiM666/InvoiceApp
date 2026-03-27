namespace InvoiceApp.Models
{
    public class TaxRate
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public double Percentage { get; set; }
    }
}