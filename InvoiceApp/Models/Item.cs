namespace InvoiceApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? HSN_SAC { get; set; }

        public int TaxRateId { get; set; }

        public TaxRate? TaxRate { get; set; }
    }
}