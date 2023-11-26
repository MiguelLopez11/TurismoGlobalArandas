using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Files
    {
        [Key]
        public int FileId { get; set; }
        public string GuidName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime UploadDate { get; set; }
        public int PaymentProviderId { get; set; }
        [ForeignKey("PaymentProviderId")]
        public PaymentProviders? PaymentProviders { get; set; }
    }
}
