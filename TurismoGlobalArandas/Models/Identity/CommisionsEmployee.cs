namespace TurismoGlobalArandas.Models.Identity
{
    public class CommisionsEmployee
    {
        public string? EmployeeName { get; set; }
        public decimal? Commission { get; set; }
        public string? TypeReservation { get; set; }
        public string? InvoiceReservation { get; set; }
        public bool isDeleted { get; set; }
        public bool isSoldOut { get; set; }
        public DateTime? DateSale { get; set; }

    }
}
