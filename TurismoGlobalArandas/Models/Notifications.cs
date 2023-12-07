using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Notifications
    {
        [Key]
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool IsReaded { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
