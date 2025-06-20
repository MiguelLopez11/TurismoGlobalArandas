﻿using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class TypeReservationGrupal
    {
        [Key] 
        public int TypeReservationGrupalId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
