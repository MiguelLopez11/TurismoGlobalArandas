﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationVehicle
    {
        [Key]
        public int ReservationVehicleId { get; set; }
        public string? Invoice { get; set; }
        public int? CustomerId { get; set; } = null;
        public DateTime? DateSale { get; set; }
        public DateTime? DateTravel { get; set; }
        public DateTime? PaymentLimitDate { get; set; } = null;
        public DateTime? PaymentLimitDateProvider { get; set; } = null;
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
        public int? ProviderId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PricePublic { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PriceNeto { get; set; }
        public string? Description { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsReservadedByHotel { get; set; }
        public int? EmployeeId { get; set; } = null;
        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }

        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }

    }
}
