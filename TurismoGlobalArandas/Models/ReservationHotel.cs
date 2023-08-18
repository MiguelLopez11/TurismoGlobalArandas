using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationHotel
    {
        [Key]
        public int ReservationHotelId { get; set; }
        public string? ReservationInvoice { get; set; } = null;
        public DateTime? TravelDate { get; set; } = null;
        public string? TypeHabitation { get; set; } = null;
        public int? NumberHabitations { get; set; } = null;
        public string? PhoneContact { get; set; } = null;   
        public string? Observations { get; set; } = null;
        public string? GroupCoordinator { get; set; } = null;
        public string? PhoneCoordinator { get; set; } = null;
        public DateTime? DateSale { get; set; } = null;
        public string? Promoter { get; set; } = null;
        public string? PaymentPeriod { get; set; } = null;
        public DateTime? PaymentLimitDate { get; set; } = null;
        public int? TypeReservationId { get; set; } = null;
        public int? EmployeeId { get; set; } = null;
        public int? CustomerId { get; set; } = null;
        public int? HotelId { get; set; } = null;
        public int? HabitationsReservationId { get; set; } = null;
        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }
        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        [ForeignKey("HotelId")]
        public Hotels? Hotels { get; set; }
        [ForeignKey("TypeReservationId")]
        public TypeReservation? TypeReservation { get; set; }
        [ForeignKey("HabitationsReservationId")]
        public HabitationsReservation? HabitationsReservation { get; set; }
    }
}
