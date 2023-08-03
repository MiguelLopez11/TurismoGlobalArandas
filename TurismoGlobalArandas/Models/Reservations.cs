using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }
        public string ReservationInvoice {get;set;}
        public DateTime TravelDate { get; set; }
        public string TypeHabitation { get; set; }
        public string Observations { get; set; }
        public int TypeReservationId {get;set;}
        public DateTime DateSale { get; set; }
        public string Promoter { get; set; }
        public string PaymentPeriod { get; set; }
        public DateTime PaymentLimitDate { get; set; }
        public int EmployeeId { get; set; } 
        public int CustomerId { get;set; }
        public int HotelId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }
        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        [ForeignKey("HotelId")]
        public Hotels? Hotels { get; set; }
        [ForeignKey("TypeReservationId")]
        public TypeReservation? TypeReservation { get; set; }
    }
}
