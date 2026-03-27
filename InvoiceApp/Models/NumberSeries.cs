namespace InvoiceApp.Models
{
    public class NumberSeries
    {
        public int Id { get; set; }

        public string? Prefix { get; set; }

        public int CurrentNumber { get; set; }

        public int Year { get; set; }
    }
}